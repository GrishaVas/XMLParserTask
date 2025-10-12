using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMLTask.DataProcessor.Infrastructure.Entities;

namespace XMLTask.DataProcessor.Infrastructure.Configurations
{
    public class InstrumentStatusConfiguration : IEntityTypeConfiguration<InstrumentStatus>
    {
        public void Configure(EntityTypeBuilder<InstrumentStatus> builder)
        {
            builder
                .HasKey(@is => @is.Id);

            builder
                .HasIndex(@is => @is.PackageID)
                .IsUnique();

            builder
                .HasMany(@is => @is.DeviceStatuses)
                .WithOne()
                .HasForeignKey(ds => ds.InstrumentStatusId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
