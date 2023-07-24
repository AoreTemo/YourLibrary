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

    public async Task<bool> AddAsync(Author author)
    {
        await _context.AddAsync(author);

        return await SaveAsync();
    }

    public async Task<bool> AddRangeAsync(IEnumerable<Author> authors)
    {
        await _context.AddRangeAsync(authors);

        return await SaveAsync();
    }

    public async Task<Author> GetByIdAsync(int id)
    {
        var author = await _context.Authors.FindAsync(id) ??
                   await _context.Authors.FirstAsync();
        
        return author;
    }

    public async Task<IEnumerable<Author>> GetAllAsync()
    {
        var authors = await _context.Authors.ToListAsync();

        return authors;
    }

    public async Task<bool> DeleteAsync(Author author)
    {
        _context.Authors.Remove(author);

        return await SaveAsync();
    }

    public async Task<bool> DeleteRangeAsync(IEnumerable<Author> authors)
    {
        _context.Authors.RemoveRange(authors);

        return await SaveAsync();
    }

    public async Task<bool> DeleteAllAsync()
    {
        var authors = await _context.Authors.ToListAsync();
        _context.Authors.RemoveRange(authors);

        return await SaveAsync();
    }

    private async Task<bool> SaveAsync()
    {
        var countOfChanges = await _context.SaveChangesAsync();
        var changesExist = countOfChanges > 0;

        return changesExist;
    }
}