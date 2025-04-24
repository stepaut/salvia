using System;
using System.Threading.Tasks;
using static salvia.Telegram.BotResources;

namespace salvia.Telegram.Handlers.CommandHanblers;

internal sealed class AddTempCommandHandler : BotCommandHanblerBase
{
    public AddTempCommandHandler(BotCommandEnviroment parameters) : base(parameters)
    {
    }

    protected override async Task Hanlde()
    {
        if (float.TryParse(_parameters.Parameter, out var temp))
        {
            var diseaseCreated = await _temperatureService.AddTemperature(DateTime.Now, temp, _parameters.UserId);

            _reply = diseaseCreated ? $"{R_DISEASE_STARTED}\n{R_TEMP_ADDED}" : R_TEMP_ADDED;
        }
        else
        {
            _reply = R_DEFAULT;
        }
    }
}
