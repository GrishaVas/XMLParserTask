using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace XMLTask.Services.Models.XML
{
    [XmlRoot(ElementName = "RapidControlStatus")]
    public class RapidControlStatus : IXmlSerializable
    {

        public CombinedStatus CombinedStatus { get; set; }

        public XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        public void ReadXml(XmlReader reader)
        {
            var xmlString = reader.ReadElementContentAsString();

            var types = new List<Type>
            {
                typeof(CombinedOvenStatus),
                typeof(CombinedSamplerStatus),
                typeof(CombinedPumpStatus)
            };

            var ser = getSerializerOfXml(types, xmlString);
            var combinedStatus = ser?.Deserialize(XmlReader.Create(new StringReader(xmlString))) ?? new CombinedOvenStatus();

            CombinedStatus = (CombinedStatus)combinedStatus;
        }

        public void WriteXml(XmlWriter writer)
        {
            throw new NotImplementedException();
        }

        private XmlSerializer getSerializerOfXml(List<Type> types, string xmlString)
        {
            foreach (var type in types)
            {
                var ser = new XmlSerializer(type);

                if (ser.CanDeserialize(XmlReader.Create(new StringReader(xmlString))))
                {
                    return ser;
                }
            }

            return null;
        }
    }
}