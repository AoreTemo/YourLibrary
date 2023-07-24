using Microsoft.AspNetCore.Mvc;

namespace YourLibrary.Controllers
{
    public class AddABookController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
