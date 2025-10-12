using Microsoft.Extensions.Options;
using XMLTask.Services;
using XMLTask.Services.Models.Enums;
using XMLTask.Services.Models.XML;
using XMLTask.XMLParser.Configurations;
using XMLTask.XMLParser.Services;

namespace XMLTask.XMLParser
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ParserConfiguration _parserConfiguration;
        private readonly IServiceProvider _serviceProvider;

        public Worker(ILogger<Worker> logger, IOptions<ParserConfiguration> parserConfiguration, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _parserConfiguration = parserConfiguration.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var xmlParserService = scope.ServiceProvider.GetService<XMLParserService>();
                    var rabbitMQService = scope.ServiceProvider.GetService<RabbitMQService>();
                    var instrumentStatuses = xmlParserService.GetInstrumentStatuses(_parserConfiguration.XMLsFolder);

                    await rabbitMQService.Connect();
                    _logger.LogInformation($"RabbitMqService connected.");
                    await sendInstrumentStatuses(rabbitMQService, instrumentStatuses);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error occured.");
                }

                _logger.LogInformation($"RabbitMqService disconnected.");

                await Task.Delay(1000, stoppingToken);
            }
        }

        private async Task sendInstrumentStatuses(RabbitMQService rabbitMQService, IEnumerable<InstrumentStatus> instrumentStatuses)
        {
            foreach (var instrumentStatus in instrumentStatuses)
            {

                updateModuleStates(instrumentStatus);
                await rabbitMQService.SendInstrumentStatus(instrumentStatus);

                _logger.LogInformation($"InstrumentStatus with PackageID = {instrumentStatus.PackageID} sended.");
            }
        }

        private void updateModuleStates(InstrumentStatus instrumentStatus)
        {
            var random = new Random();
            var moduleStates = instrumentStatus.DeviceStatuses.Select(ds => ds.RapidControlStatus.CombinedStatus.ModuleState);

            _logger.LogInformation($"Old ModuleStates: {string.Join(" ", moduleStates)}");

            instrumentStatus.DeviceStatuses.ForEach((ds) =>
            {
                ds.RapidControlStatus.CombinedStatus.ModuleState = (ModuleState)random.Next((int)ModuleState.Offline + 1);
            });

            moduleStates = instrumentStatus.DeviceStatuses.Select(ds => ds.RapidControlStatus.CombinedStatus.ModuleState);
            _logger.LogInformation($"New ModuleStates: {string.Join(" ", moduleStates)}");
        }
    }
}
