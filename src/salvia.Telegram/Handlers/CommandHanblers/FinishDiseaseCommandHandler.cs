using System;
using System.Threading.Tasks;
using static salvia.Telegram.BotResources;

namespace salvia.Telegram.Handlers.CommandHanblers;

internal sealed class FinishDiseaseCommandHandler : BotCommandHanblerBase
{
    public FinishDiseaseCommandHandler(BotCommandEnviroment parameters) : base(parameters)
    {
    }

    protected override async Task Hanlde()
    {
        var diseaseFinished = await _diseaseService.FinishCurrentDisease(DateTime.Now, _parameters.UserId);
        _reply = diseaseFinished ? R_DISEASE_FINISHD : R_DISEASE_ALREADY_FINISHD;
    }
}
