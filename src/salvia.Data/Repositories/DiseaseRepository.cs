using Mapster;
using Microsoft.EntityFrameworkCore;
using salvia.Data.Dto;
using salvia.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace salvia.Data.Repositories;

internal class DiseaseRepository : IDiseaseRepository
{
    private readonly DiseaseDbContext _context;
    

    public DiseaseRepository(DiseaseDbContext context)
    {
        _context = context;
    }


    public async Task Create(DiseaseDto item)
    {
        var entity = item.Adapt<DiseaseEntity>();

        await _context.Diseases
            .AddAsync(entity);
    }

    public async Task Delete(int id)
    {
        await _context.Diseases
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<DiseaseDto?> TryGetItem(int id)
    {
        var entity = await _context.Diseases
            .AsNoTracking()
            .Include(x => x.Temperatures)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (entity is null)
        {
            return null;
        }

        return CreateDiseaseDto(entity);
    }

    public Task Update(DiseaseDto item)
    {
        var entity = item.Adapt<DiseaseEntity>();

        _context.Diseases
            .Update(entity);

        return Task.CompletedTask;
    }

    public async Task<ICollection<DiseaseDto>> GetAll(long commonKey)
    {
        var entiies = await _context.Diseases
                            .Where(x => x.UserId == commonKey)
                            .Include(x => x.Temperatures)
                            .ToListAsync();

        return entiies.Select(CreateDiseaseDto).ToList();
    }

    private static DiseaseDto CreateDiseaseDto(DiseaseEntity entity)
    {
        var dto = entity.Adapt<DiseaseDto>();
        dto.TempsCount = entity.Temperatures.Count;

        return dto;
    }
}
