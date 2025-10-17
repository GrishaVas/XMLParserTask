using Microsoft.Extensions.Options;
using XMLTask.Services;
using XMLTask.XMLParser;
using XMLTask.XMLParser.Configurations;
using XMLTask.XMLParser.Services;

var builder = Host.CreateApplicationBuilder(args);
var parserConfiguration = builder.Configuration.GetSection(nameof(ParserConfiguration));

builder.Services.AddHostedService<Worker>();
builder.Services.Configure<ParserConfiguration>(parserConfiguration);
builder.Services.Configure<RabbitMQConfiguration>(parserConfiguration.GetSection(nameof(RabbitMQConfiguration)));
builder.Services.AddTransient<XMLInstrumentStatusSerializer>();
builder.Services.AddTransient<XMLParserService>();
builder.Services.AddTransient(sp => new RabbitMQService(sp.GetService<IOptions<RabbitMQConfiguration>>().Value));
builder.Services.AddLogging(logging =>
    logging.AddSimpleConsole(options =>
    {
        options.SingleLine = true;
    })
);

var host = builder.Build();

host.Run();
