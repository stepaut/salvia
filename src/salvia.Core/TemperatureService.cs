using salvia.Data.Dto;
using salvia.Data.Repositories;

namespace salvia.Core;

internal class TemperatureService(ITemperatureRepository _temperatureRepository, IDiseaseService _diseaseService, IDiseaseRepository _diseaseRepository) : ITemperatureService
{
    public async Task AddTemperature(DateTime date, float temperature, long user)
    {
        var currentDisease = await _diseaseService.GetCurrentDisease(user);
        currentDisease ??= await _diseaseService.AddNewAndFinishCurrentDisease(date, user);

        var temperatureDto = new TemperatureDto()
        {
            Date = date,
            Temperature = temperature,
            DiseaseId = currentDisease.Id
        };

        await _temperatureRepository.Create(temperatureDto);
        await _temperatureRepository.Save();
    }

    public async Task<ICollection<TemperatureDto>> GetAllTemperaturesInDisease(int disease, long user)
    {
        var diseaseDto = await _diseaseRepository.TryGetItem(disease);
        if (diseaseDto is null)
        {
            throw new Exception($"Disease with id {disease} doesn't exist.");
        }

        if (diseaseDto.Id != user)
        {
            throw new Exception($"Disease with id {disease} doesn't belong to user {user}.");
        }

        return await _temperatureRepository.GetAll(disease);
    }
}
