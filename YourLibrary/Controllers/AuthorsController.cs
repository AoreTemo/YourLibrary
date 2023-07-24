using Microsoft.AspNetCore.Mvc;

namespace YourLibrary.Controllers
{
    public class AuthorsController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}
