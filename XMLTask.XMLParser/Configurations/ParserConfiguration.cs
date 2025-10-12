using XMLTask.Services;

namespace XMLTask.XMLParser.Configurations
{
    public class ParserConfiguration
    {
        public string XMLsFolder { get; set; }
        public RabbitMQConfiguration RabbitMQConfiguration { get; set; }
    }
}
