using salvia.Data.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace salvia.Data.Repositories;

internal class TemperatureRepository : ITemperatureRepository
{
    public Task Create(TemperatureDto item)
    {
        throw new System.NotImplementedException();
    }

    public Task Delete(int id)
    {
        throw new System.NotImplementedException();
    }

    public Task Save()
    {
        throw new System.NotImplementedException();
    }

    public Task<TemperatureDto?> TryGetItem(int id)
    {
        throw new System.NotImplementedException();
    }

    public Task Update(TemperatureDto item)
    {
        throw new System.NotImplementedException();
    }

    public Task<ICollection<TemperatureDto>> GetAll(int commonKey)
    {
        throw new System.NotImplementedException();
    }
}

