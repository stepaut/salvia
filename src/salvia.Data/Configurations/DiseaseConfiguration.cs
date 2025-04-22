using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using salvia.Data.Entities;

namespace salvia.Data.Configurations;

internal class DiseaseConfiguration : IEntityTypeConfiguration<DiseaseEntity>
{
    public void Configure(EntityTypeBuilder<DiseaseEntity> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
