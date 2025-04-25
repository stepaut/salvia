using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using salvia.Telegram.Handlers;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using static salvia.Telegram.BotResources;

namespace salvia.Telegram;

internal class TelegramApi : ITelegramApi
{
    private readonly ITelegramBotClient _botClient;
    private readonly ReceiverOptions _receiverOptions;
    private readonly IServiceProvider _serviceProvider;
    private readonly IUserCache _userCache;



    public TelegramApi(IOptions<BotConfiguration> botConfiguration, IServiceProvider serviceProvider)
    {
        _botClient = new TelegramBotClient(botConfiguration.Value.Token);

        _receiverOptions = new ReceiverOptions
        {
            AllowedUpdates =
            [
                UpdateType.Message,
            ],
            DropPendingUpdates = true,
        };

        _serviceProvider = serviceProvider;
        _userCache = serviceProvider.GetRequiredService<IUserCache>();
    }


    public async Task Run()
    {
        using var cts = new CancellationTokenSource();

        _botClient.StartReceiving(UpdateHandler, ErrorHandler, _receiverOptions, cts.Token);

        var me = await _botClient.GetMe();
        Console.WriteLine($"{me.FirstName} is running now!");

        await Task.Delay(-1);
    }

    private async Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var chat = update.Message?.Chat;
        if (chat is null)
        {
            return;
        }

        if (_userCache.InBan(chat.Id))
        {
            return;
        }

        await using var scope = _serviceProvider.CreateAsyncScope();
        var handler = scope.ServiceProvider.GetRequiredService<ITelegramUpdateHandler>();
        var response = await HandleUpdate(update, handler);

        if (!string.IsNullOrEmpty(response.Message))
        {
            await _botClient.SendMessage(chat, response.Message);
        }

        if (response.Image is not null)
        {
            using var stream = new MemoryStream(response.Image);
            await _botClient.SendPhoto(chat, stream);
        }
    }

    private async Task<BotResponse> HandleUpdate(Update update, ITelegramUpdateHandler handler)
    {
        try
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                    return await ResolveMessage(update, handler);
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

    private async Task<BotResponse> ResolveMessage(Update update, ITelegramUpdateHandler handler)
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

        var userInfo = await handler.GetUserInfo(user);
        if (_userCache.InBan(user.Id))
        {
            return new BotResponse() { Message = R_NOT_ALLOWED };
        }

        switch (message.Type)
        {
            case MessageType.Text:
                return await ResolveTextMessage(user, message.Text, handler);
            case MessageType.Document:
                if (!userInfo.IsAllowedToLoadFiles)
                {
                    return new BotResponse() { Message = R_NOT_ALLOWED_UPLOAD };
                }
                return await ResolveDocumentMessage(user, message.Document, handler);
            default:
                return BotResponse.Default;
        }
    }

    private async Task<BotResponse> ResolveTextMessage(User user, string? message, ITelegramUpdateHandler handler)
    {
        var elements = message?.Split(' ');
        if (elements is null || elements.Length == 0)
        {
            return BotResponse.Default;
        }

        var command = elements[0];
        var parameter = elements.Length >= 2 ? elements[1] : null;

        return await handler.HandleCommand(user, command, parameter);
    }

    private async Task<BotResponse> ResolveDocumentMessage(User user, Document? document, ITelegramUpdateHandler handler)
    {
        if (document is null)
        {
            return BotResponse.Default;
        }

        await using var ms = new MemoryStream();
        var tgFile = await _botClient.GetInfoAndDownloadFile(document.FileId, ms);
        ms.Position = 0;

        return await handler.HandleDocument(user, ms);
    }

    private Task ErrorHandler(ITelegramBotClient botClient, Exception error, CancellationToken cancellationToken)
    {
        var ErrorMessage = error switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => error.ToString()
        };

        Console.WriteLine(ErrorMessage);
        return Task.CompletedTask;
    }
}
