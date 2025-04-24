using salvia.Data.Dto;
using System.Linq;
using System.Threading.Tasks;
using static salvia.Telegram.BotResources;

namespace salvia.Telegram.Handlers.CommandHanblers;

internal sealed class GetTempsCommandHandler : BotCommandHanblerBase
{
    public GetTempsCommandHandler(BotCommandEnviroment parameters) : base(parameters)
    {
    }

    protected override async Task Hanlde()
    {
        DiseaseDto? findedDis = null;

        if (_parameters.Parameter is not null)
        {
            // request specific dis
            if (int.TryParse(_parameters.Parameter, out var disId))
            {
                var dis = await _diseaseService.GetDiseaseById(disId, _parameters.UserId);
                if (dis is null)
                {
                    _reply = R_DISEASE_NOT_FOUND;
                    return;
                }

                findedDis = dis;
            }
            else
            {
                _reply = R_DEFAULT;
                return;
            }
        }
        else
        {
            // request current dis
            var currentDis = await _diseaseService.GetCurrentDisease(_parameters.UserId);
            if (currentDis is null)
            {
                _reply = R_DISEASE_NOT_STARTED;
                return;
            }
            findedDis = currentDis;
        }

        var temps = await _temperatureService.GetAllTemperaturesInDisease(findedDis.Id, _parameters.UserId);
        _reply = temps.Count > 0 ? string.Join("\n", temps.OrderBy(x => x.Date)) : R_NO_TEMPS;
    }
}
