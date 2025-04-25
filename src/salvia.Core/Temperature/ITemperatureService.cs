using salvia.Data.Dto;

namespace salvia.Core.Temperature;

public interface ITemperatureService
{
    Task<AddingTemperatureResponse> AddTemperature(DateTime date, float temperature, long user);

    Task<ICollection<TemperatureDto>> GetAllTemperaturesInDisease(int disease, long user);
}
