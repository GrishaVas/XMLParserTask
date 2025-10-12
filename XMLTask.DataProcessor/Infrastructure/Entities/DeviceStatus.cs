using System.ComponentModel.DataAnnotations;

namespace XMLTask.DataProcessor.Infrastructure.Entities
{
    public class DeviceStatus
    {
        public Guid Id { get; set; }
        [MaxLength(1024)]
        public string ModuleCategoryID { get; set; }
        public int IndexWithinRole { get; set; }
        public Guid InstrumentStatusId { get; set; }
        public RapidControlStatus RapidControlStatus { get; set; }
    }
}
