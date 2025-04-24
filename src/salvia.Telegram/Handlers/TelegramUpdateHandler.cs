using salvia.Core;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using static salvia.Telegram.BotResources;

namespace salvia.Telegram.Handlers;

internal class TelegramUpdateHandler : ITelegramUpdateHandler
{
    private readonly IDiseaseService _diseaseService;
    private readonly ITemperatureService _temperatureService;
    private readonly IUserService _userService;


    public TelegramUpdateHandler(IDiseaseService diseaseService, ITemperatureService temperatureService, IUserService userService)
    {
        _diseaseService = diseaseService;
        _temperatureService = temperatureService;
        _userService = userService;
    }


    public async Task<BotResponse> HandleUpdate(Update update)
    {
        try
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                    return await ResolveMessage(update);
                case UpdateType.CallbackQuery:
                    //return await ResolveCallbackQuery(update);
                default:
                    throw new NotImplementedException();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        return new BotResponse();
    }

    private async Task<BotResponse> ResolveTextMessage(User user, string? message)
    {
        if (!await ValidateUser(user))
        {
            return new BotResponse() { Message = R_NOT_ALLOWED };
        }

        var elements = message?.Split(' ');
        if (elements is null || elements.Length == 0)
        {
            return new BotResponse() { Message = R_DEFAULT };
        }

        var command = elements[0];
        var parameter = elements.Length >= 2 ? elements[1] : null;

        var parameters = new BotCommandParameters()
        {
            Command = command,
            Parameter = parameter,
            UserId = user.Id,
        };

        var enviroment = new BotCommandEnviroment(parameters, _diseaseService, _temperatureService);

        var handler = BotCommandHanblerFactory.Create(enviroment);
        var response = await handler.HandleCommand();

        return response;
    }

    private async Task<BotResponse> ResolveMessage(Update update)
    {
        var message = update.Message;
        if (message is null)
        {
            throw new Exception("message is null.");
        }

        var user = message.From;
        if (user is null)
        {
            throw new Exception("user is null.");
        }

        switch (message.Type)
        {
            case MessageType.Text:
                return await ResolveTextMessage(user, message.Text);
            default:
                return new BotResponse() { Message = R_DEFAULT };
        }
    }

    private async Task<bool> ValidateUser(User user)
    {
        var userWhiteListed = await _userService.IsUserWhiteListed(user.Id);
        return userWhiteListed;
    }
}
