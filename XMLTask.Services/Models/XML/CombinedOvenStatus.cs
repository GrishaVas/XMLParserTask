using System.Xml.Serialization;

namespace XMLTask.Services.Models.XML
{
    [XmlRoot(ElementName = "CombinedOvenStatus")]
    public class CombinedOvenStatus : CombinedStatus
    {
        [XmlElement(ElementName = "UseTemperatureControl")]
        public bool UseTemperatureControl { get; set; }

        [XmlElement(ElementName = "OvenOn")]
        public bool OvenOn { get; set; }

        [XmlElement(ElementName = "Temperature_Actual")]
        public double TemperatureActual { get; set; }

        [XmlElement(ElementName = "Temperature_Room")]
        public double TemperatureRoom { get; set; }

        [XmlElement(ElementName = "MaximumTemperatureLimit")]
        public int MaximumTemperatureLimit { get; set; }

        [XmlElement(ElementName = "Valve_Position")]
        public int ValvePosition { get; set; }

        [XmlElement(ElementName = "Valve_Rotations")]
        public int ValveRotations { get; set; }

        [XmlElement(ElementName = "Buzzer")]
        public bool Buzzer { get; set; }
    }
}
