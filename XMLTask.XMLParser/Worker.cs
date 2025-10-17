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
        private readonly RabbitMQService _rabbitMQService;
        private readonly XMLParserService _xMLParserService;

        public Worker(ILogger<Worker> logger, IOptions<ParserConfiguration> parserConfiguration, XMLParserService xMLParserService, RabbitMQService rabbitMQService)
        {
            _logger = logger;
            _parserConfiguration = parserConfiguration.Value;
            _rabbitMQService = rabbitMQService;
            _xMLParserService = xMLParserService;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await _rabbitMQService.Connect();

            await base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var instrumentStatuses = _xMLParserService.GetInstrumentStatuses(_parserConfiguration.XMLsFolder);

                    _logger.LogInformation($"RabbitMqService connected.");
                    await sendInstrumentStatuses(instrumentStatuses);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error occured.");
                }

                _logger.LogInformation($"RabbitMqService disconnected.");

                await Task.Delay(1000, stoppingToken);
            }
        }

        private async Task sendInstrumentStatuses(IEnumerable<InstrumentStatus> instrumentStatuses)
        {
            foreach (var instrumentStatus in instrumentStatuses)
            {

                updateModuleStates(instrumentStatus);
                await _rabbitMQService.SendInstrumentStatus(instrumentStatus);

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
