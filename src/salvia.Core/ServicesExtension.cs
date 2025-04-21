using Microsoft.Extensions.DependencyInjection;

namespace salvia.Core;

public static class ServicesExtension
{
    public static void AddCoreServices(this IServiceCollection services)
    {
        services.AddTransient<IDiseaseService, DiseaseService>();
        services.AddTransient<ITemperatureService, TemperatureService>();
    }
}
