using salvia.Data.Dto;

namespace salvia.Core;

public interface ITemperatureService
{
    Task AddTemperature(DateTime date, float temperature, long user);
    Task<ICollection<TemperatureDto>> GetAllTemperaturesInDisease(int disease, long user);
}
