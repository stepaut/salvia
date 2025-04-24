using salvia.Telegram.Handlers.CommandHanblers;
using static salvia.Telegram.BotResources;

namespace salvia.Telegram.Handlers;

internal static class BotCommandHanblerFactory
{
    public static BotCommandHanblerBase Create(BotCommandEnviroment enviroment)
    {
        switch (enviroment.Parameters.Command)
        {
            case C_START:
                return new StartCommandHandler(enviroment);
            case C_START_DISEASE:
                return new StartDiseaseCommandHandler(enviroment);
            case C_FINISH_DISEASE:
                return new FinishDiseaseCommandHandler(enviroment);
            case C_ADD_TEMP:
                return new AddTempCommandHandler(enviroment);
            case C_GET_TEMPS:
                return new GetTempsCommandHandler(enviroment);
            case C_GET_ALL_DISEASES:
                return new GetAllDiseasesCommandHandler(enviroment);
            default:
                return new DefaultCommandHanbler(enviroment);
        }
    }
}
