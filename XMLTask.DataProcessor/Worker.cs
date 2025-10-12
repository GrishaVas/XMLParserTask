using AutoMapper;
using Microsoft.EntityFrameworkCore;
using XMLTask.DataProcessor.Infrastructure;
using XMLTask.DataProcessor.Infrastructure.Entities;
using XMLTask.Services;

namespace XMLTask.DataProcessor
{
    public class Worker : IHostedService
    {
        private readonly ILogger<Worker> _logger;
        private readonly RabbitMQService _rabbitMQService;
        private readonly IMapper _mapper;
        private readonly IServiceProvider _serviceProvider;

        public Worker(ILogger<Worker> logger, RabbitMQService rabbitMQService, IMapper mapper, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _rabbitMQService = rabbitMQService;
            _mapper = mapper;
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _rabbitMQService.Connect();

            _logger.LogInformation($"RabbitMqService connected.");

            await _rabbitMQService.OnReceivingInstrumentStatus(async (instrumentStatus) =>
            {
                await onReceivingInstrumentStatus(instrumentStatus);
            },
            (ex) =>
            {
                _logger.LogError(ex, $"Error occured.");
            });
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _rabbitMQService.Dispose();
            _logger.LogInformation($"RabbitMqService disconnected.");

            return Task.CompletedTask;
        }

        private async Task onReceivingInstrumentStatus(Services.Models.XML.InstrumentStatus instrumentStatus)
        {
            if (instrumentStatus == null)
            {
                throw new Exception("InstrumentStatus cannot be null.");
            }

            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<XMLTaskDbContext>();

            _logger.LogInformation($"InstrumentStatus with PackageID = {instrumentStatus.PackageID} recieved.");

            var newInstrumentStatus = _mapper.Map<InstrumentStatus>(instrumentStatus);

            var instrumentStatusFromDb = await dbContext.InstrumentStatuses
                .Include(@is => @is.DeviceStatuses)
                .ThenInclude(ds => ds.RapidControlStatus)
                .ThenInclude(rcs => rcs.CombinedStatus)
                .SingleOrDefaultAsync(@is => @is.PackageID == newInstrumentStatus.PackageID);

            if (instrumentStatusFromDb == null)
            {
                dbContext.InstrumentStatuses.Add(newInstrumentStatus);
                _logger.LogInformation($"Added InstrumentStatus with PackageID = {instrumentStatus.PackageID}.");
            }
            else
            {
                updateInstrumentStatus(instrumentStatusFromDb, newInstrumentStatus);
                _logger.LogInformation($"Updated ModuleStates for InstrumentStatus with PackageID = {instrumentStatus.PackageID}.");
            }

            dbContext.SaveChanges();
        }

        private void updateInstrumentStatus(InstrumentStatus instrumentStatusDest, InstrumentStatus instrumentStatusSrc)
        {
            foreach (var deviceStatus in instrumentStatusDest.DeviceStatuses)
            {
                var newDeviceStatus = instrumentStatusSrc.DeviceStatuses.SingleOrDefault(ds => ds.ModuleCategoryID == deviceStatus.ModuleCategoryID);

                if (newDeviceStatus != null)
                {
                    deviceStatus.RapidControlStatus.CombinedStatus.ModuleState = newDeviceStatus.RapidControlStatus.CombinedStatus.ModuleState;
                }
            }
        }
    }
}
