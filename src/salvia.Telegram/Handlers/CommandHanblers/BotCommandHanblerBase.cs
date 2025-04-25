using salvia.Core.Disease;
using salvia.Core.Plot;
using salvia.Core.Temperature;
using System.Threading.Tasks;

namespace salvia.Telegram.Handlers.CommandHanblers;

internal abstract class BotCommandHanblerBase
{
    protected string _reply = string.Empty;
    protected byte[]? _image;

    protected readonly BotCommandParameters _parameters;
    protected readonly IDiseaseService _diseaseService;
    protected readonly ITemperatureService _temperatureService;
    protected readonly IPlotGenerator _plotGenerator;

    protected BotCommandHanblerBase(BotCommandEnviroment enviroment)
    {
        _parameters = enviroment.Parameters;
        _diseaseService = enviroment.DiseaseService;
        _temperatureService = enviroment.TemperatureService;
        _plotGenerator = enviroment.PlotGenerator;
    }

    public async Task<BotResponse> HandleCommand()
    {
        await Hanlde();
        return CreateBotResponse();
    }

    protected abstract Task Hanlde();

    private BotResponse CreateBotResponse()
    {
        return new BotResponse { Message = _reply, Image = _image };
    }
}
