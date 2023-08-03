using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using YourLibrary.Abstractions;
using YourLibrary.Data;
using YourLibrary.Models;

namespace YourLibrary.Repository;

public class BookRepository : IRepository<Book>
{
    private readonly ApplicationDbContext _context;

    public BookRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> AddAsync(Book book)
    {
        await _context.AddAsync(book);

        return await SaveAsync();
    }

    public async Task<bool> AddRangeAsync(IEnumerable<Book> books)
    {
        await _context.AddRangeAsync(books);

        return await SaveAsync();
    }

    public async Task<Book> GetByIdAsync(int id)
    {
        var book = await _context.Books.FindAsync(id) ??
                   await _context.Books.FirstAsync();
        
        return book;
    }

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        var books = await _context.Books
            .Include(book => book.Author)
            .Include(book => book.Image)
            .ToListAsync();

        return books;
    }

    public async Task<bool> DeleteAsync(Book book)
    {
        _context.Books.Remove(book);

        return await SaveAsync();
    }

    public async Task<bool> DeleteRangeAsync(IEnumerable<Book> books)
    {
        _context.Books.RemoveRange(books);

        return await SaveAsync();
    }

    public async Task<bool> DeleteAllAsync()
    {
        var books = await _context.Books.ToListAsync();
        _context.Books.RemoveRange(books);

        return await SaveAsync();
    }

    public async Task<bool> UpdateAsync(Book book)
    {
        var existingBook = await GetByIdAsync(book.Id);

        existingBook.Name = book.Name;
        existingBook.Author.Name = book.Author.Name;

        if (book.Image != null)
        {
            if (existingBook.Image != null)
            {
                existingBook.Image.Link = book.Image.Link;
            }
            else
            {
                existingBook.Image = new Image
                {
                    Link = book.Image.Link
                };
            }
        }   

        _context.Update(existingBook);

        return await SaveAsync();
    }

    public async Task<bool> SaveAsync()
    {
        var countOfChanges = await _context.SaveChangesAsync();
        var changesExist = countOfChanges > 0;
                
        return changesExist;
    }
}
