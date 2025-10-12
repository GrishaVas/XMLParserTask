using System.Xml.Serialization;

namespace XMLTask.Services.Models.XML
{
    [XmlRoot(ElementName = "CombinedSamplerStatus")]
    public class CombinedSamplerStatus : CombinedStatus
    {
        [XmlElement(ElementName = "Status")]
        public int Status { get; set; }

        [XmlElement(ElementName = "Vial")]
        public string Vial { get; set; }

        [XmlElement(ElementName = "Volume")]
        public int Volume { get; set; }

        [XmlElement(ElementName = "MaximumInjectionVolume")]
        public int MaximumInjectionVolume { get; set; }

        [XmlElement(ElementName = "RackL")]
        public string RackL { get; set; }

        [XmlElement(ElementName = "RackR")]
        public string RackR { get; set; }

        [XmlElement(ElementName = "RackInf")]
        public int RackInf { get; set; }

        [XmlElement(ElementName = "Buzzer")]
        public bool Buzzer { get; set; }
    }
}