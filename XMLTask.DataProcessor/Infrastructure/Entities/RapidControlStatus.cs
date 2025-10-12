
namespace XMLTask.DataProcessor.Infrastructure.Entities
{
    public class RapidControlStatus
    {
        public Guid Id { get; set; }
        public CombinedStatus CombinedStatus { get; set; }
        public Guid DeviceStatusId { get; set; }
    }
}
