using Microsoft.EntityFrameworkCore;
using salvia.Data.Configurations;
using salvia.Data.Entities;

namespace salvia.Data;

public class DiseaseDbContext : DbContext
{
    internal DbSet<DiseaseEntity> Diseases { get; set; }
    internal DbSet<TemperatureEntity> Temperatures { get; set; }

    public DiseaseDbContext(DbContextOptions<DiseaseDbContext> dbOptions) : base(dbOptions)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DiseaseConfiguration());
        modelBuilder.ApplyConfiguration(new TemperatureConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
