using Microsoft.EntityFrameworkCore;
using YourLibrary.Data;
using YourLibrary.Interfaces;
using YourLibrary.Models;

namespace YourLibrary.Abstractions;

public abstract class Repository : IRepository
{
    private readonly ApplicationDbContext _context;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> AddAsync(IEntity entity)
    {
        await _context.AddAsync(entity);

        return await SaveAsync();
    }

    public async Task<bool> AddRangeAsync(IEnumerable<IEntity> entity)
    {
        await _context.AddRangeAsync(entity);

        return await SaveAsync();
    }

    public async Task<List<IEntity>> GetAllAsync()
    {
        var allEntities = await _context.Set<IEntity>().ToListAsync();

        return allEntities;
    }

    public async Task<IEntity> GetByIdAsync(int id)
    {
        var entity = await _context.Set<IEntity>().FirstOrDefaultAsync(
            item => item.Id == id
        ) ?? await _context.Set<IEntity>().LastAsync();
        
        return entity;
    }

    public async Task<IEntity> GetByNameAsync(string name)
    {
        var entity = await _context.Set<IEntity>().FirstOrDefaultAsync(
            item => item.Name == name
        ) ?? await _context.Set<IEntity>().LastAsync();
        
        return entity;
    }

    public async Task<bool> DeleteAsync(IEntity entity)
    {
        _context.Remove(entity);

        return await SaveAsync();
    }

    public async Task<bool> DeleteAllAsync()
    {
        var entities = await _context.Set<IEntity>().ToListAsync();
        _context.Set<IEntity>().RemoveRange(entities);

        return await SaveAsync();
    }

    public async Task<bool> SaveAsync()
    {
        var countOfChanges = await _context.SaveChangesAsync();
        var changesExist = countOfChanges > 0;

        return changesExist;
    }
}