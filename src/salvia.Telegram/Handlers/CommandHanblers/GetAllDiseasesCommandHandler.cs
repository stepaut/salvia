using System.Linq;
using System.Threading.Tasks;

namespace salvia.Telegram.Handlers.CommandHanblers;

internal sealed class GetAllDiseasesCommandHandler : BotCommandHanblerBase
{
    public GetAllDiseasesCommandHandler(BotCommandEnviroment parameters) : base(parameters)
    {
    }

    protected override async Task Hanlde()
    {
        var diseases = await _diseaseService.GetAllDiseases(_parameters.UserId);
        _reply = string.Join("\n", diseases.OrderBy(x => x.Start));
    }
}
