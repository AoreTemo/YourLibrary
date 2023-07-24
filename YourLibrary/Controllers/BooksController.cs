using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YourLibrary.Data;

namespace YourLibrary.Controllers;

public class BooksController : Controller
{
    private readonly ApplicationDbContext _context;

    public BooksController(ApplicationDbContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        var books = _context.Books.Include(b => b.Author).ToList();

        return View(books);
    }
}