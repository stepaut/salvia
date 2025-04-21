using salvia.Data.Dto;

namespace salvia.Data.Repositories;

public interface ITemperatureRepository : IRepository<TemperatureDto, int>, IGetAllRepository<TemperatureDto, int>;

