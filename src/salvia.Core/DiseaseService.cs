using salvia.Data.Dto;
using salvia.Data.Repositories;

namespace salvia.Core;

internal class DiseaseService(IDiseaseRepository _diseaseRepository) : IDiseaseService
{
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

    public async Task FinishCurrentDisease(DateTime end, long user)
    {
        var current = await GetCurrentDisease(user);
        if (current is null)
        {
            return;
        }

        current.End = end;

        await _diseaseRepository.Update(current);
        await _diseaseRepository.Save();
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
}
