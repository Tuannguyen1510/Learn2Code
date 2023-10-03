using Microsoft.AspNetCore.Mvc;

namespace Learn2Code.Areas.Admins.Controllers
{
    [Area("Admins")]
    public class Login : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }
    }
}
