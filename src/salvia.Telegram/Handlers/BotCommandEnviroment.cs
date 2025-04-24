using salvia.Core;

namespace salvia.Telegram.Handlers;

internal class BotCommandEnviroment
{
    public BotCommandParameters Parameters { get; }
    public IDiseaseService DiseaseService { get; }
    public ITemperatureService TemperatureService { get; }

    public BotCommandEnviroment(BotCommandParameters parameters, IDiseaseService diseaseService, ITemperatureService temperatureService)
    {
        Parameters = parameters;
        DiseaseService = diseaseService;
        TemperatureService = temperatureService;
    }
}
