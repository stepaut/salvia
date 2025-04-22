using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace salvia.Telegram;

public interface ITelegramUpdateHandler
{
    Task<BotResponse> HandleUpdate(Update update);
}
