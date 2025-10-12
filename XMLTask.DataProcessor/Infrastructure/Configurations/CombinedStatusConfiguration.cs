using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMLTask.Services.Models.Enums;

namespace XMLTask.DataProcessor.Infrastructure.Configurations
{
    public class CombinedStatusConfiguration : IEntityTypeConfiguration<Entities.CombinedStatus>
    {
        public void Configure(EntityTypeBuilder<Entities.CombinedStatus> builder)
        {
            builder
                .HasKey(cs => cs.Id);

            builder
                .HasDiscriminator(cs => cs.CombinedStatusType)
                .HasValue<Entities.CombinedStatus>(CombinedStatusType.None)
                .HasValue<Entities.CombinedOvenStatus>(CombinedStatusType.Oven)
                .HasValue<Entities.CombinedPumpStatus>(CombinedStatusType.Pump)
                .HasValue<Entities.CombinedSamplerStatus>(CombinedStatusType.Sampler);
        }
    }
}
