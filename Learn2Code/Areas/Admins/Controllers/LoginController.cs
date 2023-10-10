using Learn2Code.Areas.Admins.Models;
using Learn2Code.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Learn2Code.Areas.Admins.Controllers
{
    [Area("Admins")]
    public class LoginController : Controller
    {
        private readonly W3studyContext _context;   
        public LoginController(W3studyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Index(Login model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // trả về lỗi
            }
            /// sẽ xử lý logic phần đăng nhập tại đây
       
            var dataLogin = _context.Users.Where(x => x.Email.Equals(model.Email) && x.PassWord.Equals(model.Password)).FirstOrDefault();
            if (dataLogin != null)
            {
                // Lưu session khi đăng nhập thành công
                HttpContext.Session.SetString("AdminLogin", model.Email);
                //HttpContext. Session.SetString("AdminUsers", dataLogin. ToJson());
                return RedirectToAction("Index", "Users");
            }
            return View(model); // trả về trạng thái lỗi
        }


        [HttpPost]
        public async Task<IActionResult> SignUp([Bind("Iduser,UserName,PassWord,Email")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idcategory"] = new SelectList(_context.Categorys, "Iduser", "Iduser", user.Iduser);
            return View(user);
        }


    
    }
}
