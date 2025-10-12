using System.Xml;
using XMLTask.Services.Models.XML;

namespace XMLTask.XMLParser.Services
{
    public class XMLParserService
    {
        private readonly ILogger<Worker> _logger;
        private readonly XMLInstrumentStatusSerializer _xmlInstrumentalStatusSerializer;

        public XMLParserService(ILogger<Worker> logger, XMLInstrumentStatusSerializer xmlInstrumentalStatusSerializer)
        {
            _logger = logger;
            _xmlInstrumentalStatusSerializer = xmlInstrumentalStatusSerializer;
        }

        public IEnumerable<InstrumentStatus> GetInstrumentStatuses(string folderWithXMLs)
        {
            if (string.IsNullOrWhiteSpace(folderWithXMLs))
            {
                throw new ArgumentException($"Folder name cannot be null or empty.");
            }

            if (!Directory.Exists(folderWithXMLs))
            {
                throw new ArgumentException($"Folder '{folderWithXMLs}' does not exist.");
            }

            var fileNames = Directory.GetFiles(folderWithXMLs);
            var instrumentStatuses = new InstrumentStatus[fileNames.Length];

            Parallel.For(0, fileNames.Length, index =>
            {
                var fileName = fileNames[index];

                if (!File.Exists(fileName))
                {
                    throw new ArgumentException($"File '{fileName}' does not exist.");
                }

                using var file = File.OpenRead(fileName);

                _logger.LogInformation($"'{fileName}' file opened.");

                var xmlReader = XmlReader.Create(file);

                instrumentStatuses[index] = _xmlInstrumentalStatusSerializer.GetInstrumentStatus(xmlReader);
            });

            return instrumentStatuses;
        }
    }
}
