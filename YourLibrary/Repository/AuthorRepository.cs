using Microsoft.EntityFrameworkCore;
using YourLibrary.Abstractions;
using YourLibrary.Data;
using YourLibrary.Models;

namespace YourLibrary.Repository;

public class AuthorRepository : IRepository<Author>
{
    private readonly ApplicationDbContext _context;

    public AuthorRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> DeleteAsync(Author entity)
    {
        _context.Remove(entity);

        return await SaveAsync();
    }

    public async Task<bool> DeleteRangeAsync(IEnumerable<Author> entities)
    {
        _context.RemoveRange(entities);

        return await SaveAsync();
    }

    public async Task<bool> DeleteAllAsync()
    {
        _context.Authors.RemoveRange(_context.Authors);

        return await SaveAsync();
    }

    public async Task<bool> UpdateAsync(Author author)
    {
        _context.Update(author);

        return await SaveAsync();
    }

    public async Task<bool> SaveAsync()
    {
        var countOfChanges = await _context.SaveChangesAsync();
        var changesExist = countOfChanges > 0;
                
        return changesExist;
    }

    public async Task<bool> AddAsync(Author entity)
    {
        await _context.AddAsync(entity);

        return await SaveAsync();
    }

    public async Task<bool> AddRangeAsync(IEnumerable<Author> entities)
    {
        await _context.AddRangeAsync(entities);

        return await SaveAsync();
    }

    public async Task<Author> GetByIdAsync(int id)
    {
        var author = await _context.Authors.FindAsync(id) ??
                     await _context.Authors.FindAsync();

        return author;
    }

    public async Task<IEnumerable<Author>> GetAllAsync()
    {
        var authors = await _context.Authors.ToListAsync();

        return authors;
    }
}