using Microsoft.Extensions.DependencyInjection;
using salvia.Telegram.Handlers;

namespace salvia.Telegram;

public static class ServicesExtension
{
    public static void AddTelegramApiServices(this IServiceCollection services)
    {
        services.AddSingleton<ITelegramApi, TelegramApi>();
        services.AddTransient<ITelegramUpdateHandler, TelegramUpdateHandler>();
    }
}