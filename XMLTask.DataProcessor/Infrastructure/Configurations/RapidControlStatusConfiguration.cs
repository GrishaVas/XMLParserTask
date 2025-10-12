using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XMLTask.DataProcessor.Infrastructure.Entities;

namespace XMLTask.Infrastructure.Configurations
{
    public class RapidControlStatusConfiguration : IEntityTypeConfiguration<RapidControlStatus>
    {
        public void Configure(EntityTypeBuilder<RapidControlStatus> builder)
        {
            builder
                .HasKey(rcs => rcs.Id);

            builder
                .HasOne(rcs => rcs.CombinedStatus)
                .WithOne()
                .HasForeignKey<CombinedStatus>(cs => cs.RapidControlStatusId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
