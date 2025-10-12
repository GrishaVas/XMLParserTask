using System.Xml.Serialization;

namespace XMLTask.Services.Models.XML
{
    [XmlRoot(ElementName = "InstrumentStatus")]
    public class InstrumentStatus
    {

        [XmlElement(ElementName = "PackageID")]
        public string PackageID { get; set; }

        [XmlElement(ElementName = "DeviceStatus")]
        public List<DeviceStatus> DeviceStatuses { get; set; }

        [XmlAttribute(AttributeName = "schemaVersion")]
        public string SchemaVersion { get; set; }

    }
}
