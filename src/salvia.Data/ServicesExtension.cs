using Microsoft.Extensions.DependencyInjection;
using salvia.Data.Repositories;

namespace salvia.Data;

public static class ServicesExtension
{
    public static void AddDataServices(this IServiceCollection services)
    {
        services.AddScoped<IDiseaseRepository, DiseaseRepository>();
        services.AddScoped<ITemperatureRepository, TemperatureRepository>();
    }
}