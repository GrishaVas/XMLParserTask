using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMLTask.DataProcessor.Infrastructure.Entities;

namespace XMLTask.DataProcessor.Infrastructure.Configurations
{
    public class DeviceStatusConfiguration : IEntityTypeConfiguration<DeviceStatus>
    {
        public void Configure(EntityTypeBuilder<DeviceStatus> builder)
        {
            builder
                .HasKey(ds => ds.Id);

            builder
                .HasOne(ds => ds.RapidControlStatus)
                .WithOne()
                .HasForeignKey<RapidControlStatus>(rcs => rcs.DeviceStatusId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
