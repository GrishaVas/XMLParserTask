using System.ComponentModel.DataAnnotations;

namespace XMLTask.DataProcessor.Infrastructure.Entities
{
    public class InstrumentStatus
    {
        public Guid Id { get; set; }
        [MaxLength(1024)]
        public string PackageID { get; set; }
        public IEnumerable<DeviceStatus> DeviceStatuses { get; set; }
    }
}
