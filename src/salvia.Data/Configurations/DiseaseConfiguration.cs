using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using salvia.Data.Entities;

namespace salvia.Data.Configurations;

internal class DiseaseConfiguration : IEntityTypeConfiguration<DiseaseEntry>
{
    public void Configure(EntityTypeBuilder<DiseaseEntry> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
