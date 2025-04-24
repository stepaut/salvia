using System.Threading.Tasks;
using static salvia.Telegram.BotResources;

namespace salvia.Telegram.Handlers.CommandHanblers;

internal sealed class SimpleReplyCommandHandler : BotCommandHanblerBase
{
    public SimpleReplyCommandHandler(BotCommandEnviroment parameters) : base(parameters)
    {
    }

    protected override Task Hanlde()
    {
        switch (_parameters.Command)
        {
            case C_START:
                _reply = R_HELLO; break;
            case C_HELP:
                _reply = R_HELP; break;
            default:
                _reply = R_DEFAULT; break;
        }
        
        return Task.CompletedTask;
    }
}
