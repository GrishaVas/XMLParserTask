using System.Xml.Serialization;

namespace XMLTask.Services.Models.XML
{
    [XmlRoot(ElementName = "CombinedPumpStatus")]
    public class CombinedPumpStatus : CombinedStatus
    {
        [XmlElement(ElementName = "Mode")]
        public string Mode { get; set; }

        [XmlElement(ElementName = "Flow")]
        public int Flow { get; set; }

        [XmlElement(ElementName = "PercentB")]
        public int PercentB { get; set; }

        [XmlElement(ElementName = "PercentC")]
        public int PercentC { get; set; }

        [XmlElement(ElementName = "PercentD")]
        public int PercentD { get; set; }

        [XmlElement(ElementName = "MinimumPressureLimit")]
        public int MinimumPressureLimit { get; set; }

        [XmlElement(ElementName = "MaximumPressureLimit")]
        public double MaximumPressureLimit { get; set; }

        [XmlElement(ElementName = "Pressure")]
        public int Pressure { get; set; }

        [XmlElement(ElementName = "PumpOn")]
        public bool PumpOn { get; set; }

        [XmlElement(ElementName = "Channel")]
        public int Channel { get; set; }
    }
}