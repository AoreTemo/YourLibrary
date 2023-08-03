using Microsoft.EntityFrameworkCore;
using YourLibrary.Abstractions;
using YourLibrary.Data;
using YourLibrary.Models;

namespace YourLibrary.Repository;

public class ImageRepository : IRepository<Image>
{
    private readonly ApplicationDbContext _context;

    public ImageRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> DeleteAsync(Image entity)
    {
        _context.Remove(entity);

        return await SaveAsync();
    }

    public async Task<bool> DeleteRangeAsync(IEnumerable<Image> entities)
    {
        _context.RemoveRange(entities);

        return await SaveAsync();
    }

    public async Task<bool> DeleteAllAsync()
    {
        _context.Images.RemoveRange(_context.Images);

        return await SaveAsync();
    }

    public async Task<bool> UpdateAsync(Image image)
    {
        _context.Update(image);

        return await SaveAsync();
    }

    public async Task<bool> SaveAsync()
    {
        var countOfChanges = await _context.SaveChangesAsync();
        var changesExist = countOfChanges > 0;
                
        return changesExist;
    }

    public async Task<bool> AddAsync(Image entity)
    {
        await _context.AddAsync(entity);

        return await SaveAsync();
    }

    public async Task<bool> AddRangeAsync(IEnumerable<Image> entities)
    {
        await _context.AddRangeAsync(entities);

        return await SaveAsync();
    }

    public async Task<Image> GetByIdAsync(int id)
    {
        var image = await _context.Images.FindAsync(id) ??
                    await _context.Images.FirstAsync();

        return image;
    }

    public async Task<IEnumerable<Image>> GetAllAsync()
    {
        var images = await _context.Images.ToListAsync();

        return images;
    }
}
