using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YourLibrary.Abstractions;
using YourLibrary.Data;
using YourLibrary.Models;
using YourLibrary.Repository;

namespace YourLibrary.Controllers;

public class BooksController : Controller
{
    private readonly IRepository<Book> _bookRepository;

    public BooksController(IRepository<Book> bookRepository)
    {
        _bookRepository = bookRepository;
    }
    public async Task<IActionResult> Index()
    {
        var books = await _bookRepository.GetAllAsync();

        return View(books);
    }
}