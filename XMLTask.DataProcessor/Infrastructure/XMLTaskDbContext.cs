using Microsoft.EntityFrameworkCore;
using XMLTask.DataProcessor.Infrastructure.Entities;

namespace XMLTask.DataProcessor.Infrastructure
{
    public class XMLTaskDbContext : DbContext
    {
        public DbSet<InstrumentStatus> InstrumentStatuses { get; set; }
        public DbSet<DeviceStatus> DeviceStatuses { get; set; }
        public DbSet<RapidControlStatus> RapidControlStatuses { get; set; }
        public DbSet<CombinedStatus> CombinedStatuses { get; set; }

        public XMLTaskDbContext(DbContextOptions<XMLTaskDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .ApplyConfigurationsFromAssembly(typeof(XMLTaskDbContext).Assembly);
        }
    }
}
