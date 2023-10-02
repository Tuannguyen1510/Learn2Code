using Microsoft.AspNetCore.Mvc;

namespace Learn2Code.Areas.Admins.Controllers
{
    [Area("Admins")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
