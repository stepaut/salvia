using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using salvia.Data.Entities;

namespace salvia.Data.Configurations;

internal class TemperatureConfiguration : IEntityTypeConfiguration<TemperatureEntry>
{
    public void Configure(EntityTypeBuilder<TemperatureEntry> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(t => t.Disease)
            .WithMany(d => d.Temperatures)
            .HasForeignKey(t => t.DiseaseId);
    }
}
