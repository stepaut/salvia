using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace salvia.Telegram.Handlers;

public interface ITelegramUpdateHandler
{
    Task<BotResponse> HandleUpdate(Update update);
}
