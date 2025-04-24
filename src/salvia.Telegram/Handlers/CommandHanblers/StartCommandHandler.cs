using System.Threading.Tasks;
using static salvia.Telegram.BotResources;

namespace salvia.Telegram.Handlers.CommandHanblers;

internal sealed class StartCommandHandler : BotCommandHanblerBase
{
    public StartCommandHandler(BotCommandEnviroment parameters) : base(parameters)
    {
    }

    protected override Task Hanlde()
    {
        _reply = R_HELLO;
        return Task.CompletedTask;
    }
}
