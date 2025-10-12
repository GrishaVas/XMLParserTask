using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace XMLTask.DataProcessor.Infrastructure
{
    public class DataProcessorContextFactory : IDesignTimeDbContextFactory<XMLTaskDbContext>
    {
        public XMLTaskDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var contextOptions = new DbContextOptionsBuilder<XMLTaskDbContext>()
                .UseSqlite(config["SqLiteConnectionString"])
                .Options;

            return new XMLTaskDbContext(contextOptions);
        }
    }
}
