using Microsoft.Extensions.DependencyInjection;
using salvia.Core.Disease;
using salvia.Core.Plot;
using salvia.Core.Temperature;

namespace salvia.Core;

public static class ServicesExtension
{
    public static void AddCoreServices(this IServiceCollection services)
    {
        services.AddTransient<IPlotGenerator, PlotGenerator>();
        services.AddTransient<IDiseaseService, DiseaseService>();
        services.AddTransient<ITemperatureService, TemperatureService>();
    }
}
