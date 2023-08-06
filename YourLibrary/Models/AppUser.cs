using Microsoft.AspNetCore.Identity;
using YourLibrary.Enums;

namespace YourLibrary.Models;

public class AppUser : IdentityUser
{
    public UserRole Role { get; set; }
    public ICollection<Book> Books { get; set; } = new List<Book>();
}
