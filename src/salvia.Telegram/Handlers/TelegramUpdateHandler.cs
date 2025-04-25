using salvia.Core.Disease;
using salvia.Core.Plot;
using salvia.Core.Temperature;
using salvia.Data.Entities;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace salvia.Telegram.Handlers;

internal class TelegramUpdateHandler : ITelegramUpdateHandler
{
    private readonly IDiseaseService _diseaseService;
    private readonly ITemperatureService _temperatureService;
    private readonly IUserService _userService;
    private readonly IPlotGenerator _plotGenerator;


    public TelegramUpdateHandler(IDiseaseService diseaseService, ITemperatureService temperatureService, IUserService userService, IPlotGenerator plotGenerator)
    {
        _diseaseService = diseaseService;
        _temperatureService = temperatureService;
        _userService = userService;
        _plotGenerator = plotGenerator;
    }


    public async Task<BotResponse> HandleDocument(User user, Stream stream)
    {
        var response = await _diseaseService.ImportDisease(stream, user.Id);
        if (response.Success)
        {
            return new BotResponse() { Message = string.Format(BotResources.R_SUCCESS_IMPORT_DISEASE, response.ImportedDisease!.Id) };
        }

        return new BotResponse() { Message = response.ErrorMessage ?? BotResources.R_FAILED };
    }

    public async Task<BotResponse> HandleCommand(User user, string command, string? parameter)
    {
        var parameters = new BotCommandParameters()
        {
            Command = command,
            Parameter = parameter,
            UserId = user.Id,
        };

        var enviroment = new BotCommandEnviroment(parameters, _diseaseService, _temperatureService, _plotGenerator);

        var handler = BotCommandHanblerFactory.Create(enviroment);
        var response = await handler.HandleCommand();

        return response;
    }

    public async Task<UserDto> GetUserInfo(User user)
    {
        return await _userService.GetUserInfo(user.Id);
    }
}
