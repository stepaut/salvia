using Microsoft.Extensions.DependencyInjection;

namespace salvia.Telegram;

public static class ServicesExtension
{
    public static void AddTelegramApiServices(this IServiceCollection services)
    {
        services.AddTransient<ITelegramApi, TelegramApi>();
    }
}