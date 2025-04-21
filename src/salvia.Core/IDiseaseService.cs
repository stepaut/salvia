using salvia.Data.Dto;

namespace salvia.Core;

public interface IDiseaseService
{
    Task<DiseaseDto> AddNewAndFinishCurrentDisease(DateTime start, long user);
    Task FinishCurrentDisease(DateTime end, long user);
    Task<DiseaseDto?> GetCurrentDisease(long user);
}
