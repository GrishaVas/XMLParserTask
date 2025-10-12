using System.ComponentModel.DataAnnotations;

namespace XMLTask.DataProcessor.Infrastructure.Entities
{
    public class CombinedSamplerStatus : CombinedStatus
    {
        public int Status { get; set; }
        [MaxLength(1024)]
        public string Vial { get; set; }
        public int Volume { get; set; }
        public int MaximumInjectionVolume { get; set; }
        [MaxLength(1024)]
        public string RackL { get; set; }
        [MaxLength(1024)]
        public string RackR { get; set; }
        public int RackInf { get; set; }
        public bool Buzzer { get; set; }
    }
}