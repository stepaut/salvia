using System;
using System.Globalization;
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
        if (!float.TryParse(_parameters.Parameter?.Replace(',', '.'), CultureInfo.InvariantCulture, out var temp))
        {
            _reply = R_DEFAULT;
            return;
        }

        var response = await _temperatureService.AddTemperature(DateTime.Now, temp, _parameters.UserId);

        if (!response.Success)
        {
            _reply = response.ErrorMessage ?? R_FAILED;
            return;
        }

        _reply = string.Empty;
        if (response.DiseasesCreated)
        {
            _reply += $"{R_DISEASE_STARTED}\n";
        }
        _reply += R_TEMP_ADDED; 
    }
}
