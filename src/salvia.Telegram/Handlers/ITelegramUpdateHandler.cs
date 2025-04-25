using salvia.Data.Entities;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace salvia.Telegram.Handlers;

public interface ITelegramUpdateHandler
{
    //Task<BotResponse> HandleUpdate(Update update);

    Task<UserDto> GetUserInfo(User user);
    Task<BotResponse> HandleCommand(User user, string command, string? parameter);
    Task<BotResponse> HandleDocument(User user, Stream stream);
}
