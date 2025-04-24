using System.Threading.Tasks;
using static salvia.Telegram.BotResources;

namespace salvia.Telegram.Handlers.CommandHanblers;

internal sealed class DefaultCommandHanbler : BotCommandHanblerBase
{
    public DefaultCommandHanbler(BotCommandEnviroment parameters) : base(parameters)
    {
    }

    protected override Task Hanlde()
    {
        _reply = R_DEFAULT;
        return Task.CompletedTask;
    }
}
