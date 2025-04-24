using Mapster;
using Microsoft.EntityFrameworkCore;
using salvia.Data.Dto;
using salvia.Data.Entities;
using System.Threading.Tasks;

namespace salvia.Data.Repositories;

internal class UserRepository(DiseaseDbContext _context) : IUserRepository
{
    public async Task Create(UserDto item)
    {
        var entity = item.Adapt<UserEntity>();
        await _context.Users.AddAsync(entity);
    }

    public async Task Delete(long id)
    {
        throw new System.NotImplementedException();
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<UserDto?> TryGetItem(long id)
    {
        var entity = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return entity.Adapt<UserDto>();
    }

    public Task Update(UserDto item)
    {
        var entity = item.Adapt<UserEntity>();
        _context.Users.Update(entity);

        return Task.CompletedTask;
    }
}