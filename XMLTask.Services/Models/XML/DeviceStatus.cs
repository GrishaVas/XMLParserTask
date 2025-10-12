using System.Xml.Serialization;

namespace XMLTask.Services.Models.XML
{
    [XmlRoot(ElementName = "DeviceStatus")]
    public class DeviceStatus
    {

        [XmlElement(ElementName = "ModuleCategoryID")]
        public string ModuleCategoryID { get; set; }

        [XmlElement(ElementName = "IndexWithinRole")]
        public int IndexWithinRole { get; set; }

        [XmlElement(ElementName = "RapidControlStatus")]
        public RapidControlStatus RapidControlStatus { get; set; }
    }
}
