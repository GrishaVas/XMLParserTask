using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using XMLTask.DataProcessor;
using XMLTask.DataProcessor.Infrastructure;
using XMLTask.Services;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<Worker>();
builder.Services.Configure<RabbitMQConfiguration>(builder.Configuration.GetSection(nameof(RabbitMQConfiguration)));
builder.Services.AddAutoMapper(conf => conf.AddMaps(typeof(Program).Assembly));
builder.Services.AddSingleton(sp => new RabbitMQService(sp.GetService<IOptions<RabbitMQConfiguration>>().Value));
builder.Services.AddDbContext<XMLTaskDbContext>(opts => opts.UseSqlite(builder.Configuration.GetConnectionString("SqliteXMLTaskConnectionString")), contextLifetime: ServiceLifetime.Transient, optionsLifetime: ServiceLifetime.Transient);
builder.Services.AddLogging(logging =>
    logging.AddSimpleConsole(options =>
    {
        options.SingleLine = true;
    })
);

var host = builder.Build();
var serviceProvider = host.Services.GetService<IServiceProvider>();

using (var scope = serviceProvider.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetService<XMLTaskDbContext>();
    await dbContext.Database.MigrateAsync();
}

host.Run();
