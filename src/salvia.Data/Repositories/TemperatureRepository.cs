using Mapster;
using Microsoft.EntityFrameworkCore;
using salvia.Data.Dto;
using salvia.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace salvia.Data.Repositories;

internal class TemperatureRepository(DiseaseDbContext _context) : ITemperatureRepository
{
    public async Task<TemperatureDto> Create(TemperatureDto item)
    {
        var entity = item.Adapt<TemperatureEntity>();
        var newEntity = await _context.Temperatures.AddAsync(entity);
        await _context.SaveChangesAsync();
        return newEntity.Adapt<TemperatureDto>();
    }

    public async Task Delete(int id)
    {
        await _context.Temperatures.Where(x => x.Id == id).ExecuteDeleteAsync();
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<TemperatureDto?> TryGetItem(int id)
    {
        var entity = await _context.Temperatures.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return entity.Adapt<TemperatureDto>();
    }

    public Task Update(TemperatureDto item)
    {
        var entity = item.Adapt<TemperatureEntity>();
        _context.Temperatures.Update(entity);

        return Task.CompletedTask;
    }

    public async Task<ICollection<TemperatureDto>> GetAll(int commonKey)
    {
        return await _context.Temperatures.AsNoTracking()
                            .Where(x => x.DiseaseId == commonKey)
                            .Select(x => x.Adapt<TemperatureDto>())
                            .ToListAsync();
    }
}

