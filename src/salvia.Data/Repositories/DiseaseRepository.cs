using salvia.Data.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace salvia.Data.Repositories;

internal class DiseaseRepository(DiseaseDbContext _context) : IDiseaseRepository
{
    public Task Create(DiseaseDto item)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task Save()
    {
        throw new NotImplementedException();
    }

    public Task<DiseaseDto?> TryGetItem(int id)
    {
        throw new NotImplementedException();
    }

    public Task Update(DiseaseDto item)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<DiseaseDto>> GetAll(long commonKey)
    {
        throw new NotImplementedException();
    }
}

