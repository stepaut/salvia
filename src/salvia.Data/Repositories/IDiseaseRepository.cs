using salvia.Data.Dto;

namespace salvia.Data.Repositories;

public interface IDiseaseRepository : IRepository<DiseaseDto, int>, IGetAllRepository<DiseaseDto, long>;
