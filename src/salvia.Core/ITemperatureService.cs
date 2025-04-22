using salvia.Data.Dto;

namespace salvia.Core;

public interface ITemperatureService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="date"></param>
    /// <param name="temperature"></param>
    /// <param name="user"></param>
    /// <returns>TRUE if disease created</returns>
    Task<bool> AddTemperature(DateTime date, float temperature, long user);

    Task<ICollection<TemperatureDto>> GetAllTemperaturesInDisease(int disease, long user);
}
