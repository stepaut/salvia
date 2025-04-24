using salvia.Core;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using static salvia.Telegram.BotResources;

namespace salvia.Telegram.Handlers.CommandHanblers;

internal sealed class StartDiseaseCommandHandler : BotCommandHanblerBase
{
    public StartDiseaseCommandHandler(BotCommandEnviroment parameters) : base(parameters)
    {
    }

    protected override async Task Hanlde()
    {
        await _diseaseService.AddNewAndFinishCurrentDisease(DateTime.Now, _parameters.UserId);
        _reply = R_DISEASE_STARTED;
    }
}
