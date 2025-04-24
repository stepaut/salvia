using Mapster;
using Microsoft.EntityFrameworkCore;
using salvia.Data.Dto;
using salvia.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace salvia.Data.Repositories;

internal class DiseaseRepository(DiseaseDbContext _context) : IDiseaseRepository
{
    public async Task Create(DiseaseDto item)
    {
        var entity = item.Adapt<DiseaseEntity>();
        await _context.Diseases.AddAsync(entity);
    }

    public async Task Delete(int id)
    {
        await _context.Diseases.Where(x => x.Id == id).ExecuteDeleteAsync();
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<DiseaseDto?> TryGetItem(int id)
    {
        var entity = await _context.Diseases.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (entity is null)
        {
            return null;
        }

        var dto = entity.Adapt<DiseaseDto>();
        dto.TempsCount = entity.Temperatures.Count;

        return dto;
    }

    public Task Update(DiseaseDto item)
    {
        var entity = item.Adapt<DiseaseEntity>();
        _context.Diseases.Update(entity);

        return Task.CompletedTask;
    }

    public async Task<ICollection<DiseaseDto>> GetAll(long commonKey)
    {
        return await _context.Diseases
                            .Where(x => x.UserId == commonKey)
                            .AsNoTracking()
                            .Select(x => x.Adapt<DiseaseDto>())
                            .ToListAsync();
    }
}

