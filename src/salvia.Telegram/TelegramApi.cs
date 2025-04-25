using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using salvia.Telegram.Handlers;
using System;
using System.Collections;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace salvia.Telegram;

internal class TelegramApi : ITelegramApi
{
    private readonly ITelegramBotClient _botClient;
    private readonly ReceiverOptions _receiverOptions;
    private readonly IServiceProvider _serviceProvider;


    public TelegramApi(IOptions<BotConfiguration> botConfiguration, IServiceProvider serviceProvider)
    {
        _botClient = new TelegramBotClient(botConfiguration.Value.Token);

        _receiverOptions = new ReceiverOptions
        {
            AllowedUpdates =
            [
                UpdateType.Message,
                UpdateType.CallbackQuery,
            ],
            DropPendingUpdates = true,
        };

        _serviceProvider = serviceProvider;
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

        await using var scope = _serviceProvider.CreateAsyncScope();
        var handler = scope.ServiceProvider.GetRequiredService<ITelegramUpdateHandler>();
        var response = await handler.HandleUpdate(update);

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
