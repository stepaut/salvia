using salvia.Core.Disease;
using salvia.Core.Plot;
using salvia.Core.Temperature;

namespace salvia.Telegram.Handlers;

internal class BotCommandEnviroment
{
    public BotCommandParameters Parameters { get; }
    public IDiseaseService DiseaseService { get; }
    public ITemperatureService TemperatureService { get; }
    public IPlotGenerator PlotGenerator { get; }

    public BotCommandEnviroment(BotCommandParameters parameters, IDiseaseService diseaseService, ITemperatureService temperatureService, IPlotGenerator plotGenerator)
    {
        Parameters = parameters;
        DiseaseService = diseaseService;
        TemperatureService = temperatureService;
        PlotGenerator = plotGenerator;
    }
}
