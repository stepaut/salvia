using salvia.Data.Dto;
using salvia.Data.Repositories;

namespace salvia.Core.Disease;

internal class DiseaseService : IDiseaseService
{
    private readonly IDiseaseRepository _diseaseRepository;
    private readonly ITemperatureRepository _temperatureRepository;

    public DiseaseService(IDiseaseRepository diseaseRepository, ITemperatureRepository temperatureRepository)
    {
        _diseaseRepository = diseaseRepository;
        _temperatureRepository = temperatureRepository;
    }

    public async Task<DiseaseDto> AddNewAndFinishCurrentDisease(DateTime start, long user)
    {
        await FinishCurrentDisease(start, user);

        var disease = new DiseaseDto()
        {
            Start = start,
            UserId = user,
        };

        await _diseaseRepository.Create(disease);
        await _diseaseRepository.Save();

        var createdDisease = await GetCurrentDisease(user);

        return createdDisease!;
    }

    public async Task<bool> FinishCurrentDisease(DateTime end, long user)
    {
        var current = await GetCurrentDisease(user);
        if (current is null)
        {
            return false;
        }

        if (current.TempsCount == 0)
        {
            await _diseaseRepository.Delete(current.Id);
        }
        else
        {
            current.End = end;
            await _diseaseRepository.Update(current);
        }

        await _diseaseRepository.Save();

        return true;
    }

    public async Task<DiseaseDto?> GetCurrentDisease(long user)
    {
        var diseases = await _diseaseRepository.GetAll(user);
        if (!diseases.Any())
        {
            return null;
        }

        var last = diseases.OrderBy(x => x.Start).Last();
        if (last.End is not null)
        {
            return null;
        }

        return last;
    }

    public async Task<ICollection<DiseaseDto>> GetAllDiseases(long user)
    {
        return await _diseaseRepository.GetAll(user);
    }

    public async Task<DiseaseDto?> GetDiseaseById(int id, long user)
    {
        var dis = await _diseaseRepository.TryGetItem(id);
        if (dis is null)
        {
            return null;
        }

        if (dis.UserId != user)
        {
            return null;
        }

        return dis;
    }

    public async Task<ImportDiseaseResponse> ImportDisease(Stream stream, long user)
    {
        using var parser = new DiseaseParser();
        try
        {
            parser.Parse(stream);
        }
        catch (Exception ex)
        {
            return new ImportDiseaseResponse() { Success = false, ErrorMessage = ex.Message };
        }

        var diseaseRaw = parser.GetDisease();
        diseaseRaw.UserId = user;

        var disease = await _diseaseRepository.Create(diseaseRaw);

        var temps = parser.GetTemperatures();

        foreach (var temp in temps)
        {
            temp.DiseaseId = disease.Id;
            await _temperatureRepository.Create(temp);
        }

        return new ImportDiseaseResponse() { Success = true, ImportedDisease = disease };
    }
}
