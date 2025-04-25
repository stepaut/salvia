using salvia.Core.Disease;
using salvia.Data.Dto;
using salvia.Data.Repositories;

namespace salvia.Core.Temperature;

internal class TemperatureService : ITemperatureService
{
    private readonly ITemperatureRepository _temperatureRepository;
    private readonly IDiseaseService _diseaseService;
    private readonly IDiseaseRepository _diseaseRepository;


    public TemperatureService(ITemperatureRepository temperatureRepository, IDiseaseService diseaseService, IDiseaseRepository diseaseRepository)
    {
        _temperatureRepository = temperatureRepository;
        _diseaseService = diseaseService;
        _diseaseRepository = diseaseRepository;
    }


    public async Task<bool> AddTemperature(DateTime date, float temperature, long user)
    {
        var diseaseCreated = false;
        var currentDisease = await _diseaseService.GetCurrentDisease(user);

        if (currentDisease is null)
        {
            currentDisease = await _diseaseService.AddNewAndFinishCurrentDisease(date, user);
            diseaseCreated = true;
        }

        var temperatureDto = new TemperatureDto()
        {
            Date = date,
            Temperature = temperature,
            DiseaseId = currentDisease.Id
        };

        await _temperatureRepository.Create(temperatureDto);
        await _temperatureRepository.Save();

        return diseaseCreated;
    }

    public async Task<ICollection<TemperatureDto>> GetAllTemperaturesInDisease(int disease, long user)
    {
        var diseaseDto = await _diseaseRepository.TryGetItem(disease);
        if (diseaseDto is null)
        {
            throw new Exception($"Disease with id {disease} doesn't exist.");
        }

        if (diseaseDto.UserId != user)
        {
            throw new Exception($"Disease with id {disease} doesn't belong to user {user}.");
        }

        return await _temperatureRepository.GetAll(disease);
    }
}
