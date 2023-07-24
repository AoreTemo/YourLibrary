using Microsoft.EntityFrameworkCore;
using YourLibrary.Models;

namespace YourLibrary.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    { }

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Image> Images { get; set; }
}