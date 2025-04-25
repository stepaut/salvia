using salvia.Data.Dto;

namespace salvia.Core.Disease;

public interface IDiseaseService
{
    Task<DiseaseDto> AddNewAndFinishCurrentDisease(DateTime start, long user);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="end"></param>
    /// <param name="user"></param>
    /// <returns>FALSE if disease alredy finished</returns>
    Task<bool> FinishCurrentDisease(DateTime end, long user);

    Task<DiseaseDto?> GetCurrentDisease(long user);

    Task<DiseaseDto?> GetDiseaseById(int id, long user);

    Task<ICollection<DiseaseDto>> GetAllDiseases(long user);

    Task<ImportDiseaseResponse> ImportDisease(Stream stream, long user);
}

public class ImportDiseaseResponse : ResponseBase
{
    public DiseaseDto? ImportedDisease { get; init; }
}
