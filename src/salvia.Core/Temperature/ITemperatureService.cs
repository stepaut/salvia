using salvia.Data.Dto;

namespace salvia.Core.Temperature;

public interface ITemperatureService
{
    Task<AddingTemperatureResponse> AddTemperature(DateTime date, float temperature, long user);

    Task<ICollection<TemperatureDto>> GetAllTemperaturesInDisease(int disease, long user);

    static bool ValidateTemperature(float temperature)
    {
        if (temperature < CoreConstants.MIN_TEMP || temperature > CoreConstants.MAX_TEMP)
        {
            return false;
        }

        return true;
    }
}
