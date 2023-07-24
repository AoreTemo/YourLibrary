using Microsoft.AspNetCore.Mvc;
using YourLibrary.Abstractions;
using YourLibrary.Models;

namespace YourLibrary.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IRepository<Author> _authorRepository;

        public AuthorsController(IRepository<Author> authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<IActionResult> Index()
        {
            var authors = await _authorRepository.GetAllAsync();

            return View(authors);
        }
    }
}
