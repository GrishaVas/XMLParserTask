using System.Xml;
using System.Xml.Serialization;
using XMLTask.Services.Models.XML;

namespace XMLTask.XMLParser.Services
{
    public class XMLInstrumentStatusSerializer
    {
        private readonly ILogger<Worker> _logger;

        public XMLInstrumentStatusSerializer(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        public InstrumentStatus GetInstrumentStatus(XmlReader xmlReader)
        {
            var serializer = new XmlSerializer(typeof(InstrumentStatus));

            if (!serializer.CanDeserialize(xmlReader))
            {
                throw new Exception($"Cant't deserialize xml file.");
            }

            var instrumentStatus = (InstrumentStatus)serializer.Deserialize(xmlReader);

            _logger.LogInformation($"InstrumentalStatus with {nameof(instrumentStatus.PackageID)} = {instrumentStatus.PackageID} deserialized.");

            return instrumentStatus;
        }
    }
}
