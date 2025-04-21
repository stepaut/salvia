using Microsoft.EntityFrameworkCore;
using salvia.Data.Configurations;
using salvia.Data.Entities;

namespace salvia.Data;

public class DiseaseDbContext(DbContextOptions<DiseaseDbContext> dbOptions) : DbContext(dbOptions)
{
    internal DbSet<DiseaseEntry> Diseases { get; set; }
    internal DbSet<TemperatureEntry> Temperatures { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DiseaseConfiguration());
        modelBuilder.ApplyConfiguration(new TemperatureConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
