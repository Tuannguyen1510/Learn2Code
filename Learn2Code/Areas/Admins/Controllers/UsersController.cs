using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Learn2Code.Models;
using X.PagedList;

namespace Learn2Code.Areas.Admins.Controllers
{
    [Area("Admins")]
    public class UsersController : Controller
    {
        private readonly W3studyContext _context;

        public UsersController(W3studyContext context)
        {
            _context = context;
        }

        // GET: Admins/Users
        public async Task<IActionResult> Index(string? name, int page = 1)
        {
            //  var a = await _context.Accounts.ToListAsync();
            // nếu có tham số name trên url
            //  if (!String.IsNullOrEmpty(name))
            //  {
            //      a = await _context.Accounts.Where(c => c.FullName.Contains(name)).ToListAsync();
            //  }

            int limit = 5;


            var a = await _context.Users.OrderBy(c => c.UserName).ToPagedListAsync(page, limit);

            if (!String.IsNullOrEmpty(name))
            {
                a = await _context.Users.Where(c => c.UserName.Contains(name)).OrderBy(c => c.Iduser).ToPagedListAsync(page, limit);
            }


            ViewBag.keyword = name;
            return View(a);
        }

        // GET: Admins/Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.IdroleNavigation)
                .FirstOrDefaultAsync(m => m.Iduser == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Admins/Users/Create
        public IActionResult Create()
        {
            ViewData["Idrole"] = new SelectList(_context.Roles, "Idrole", "Idrole");
            return View();
        }

        // POST: Admins/Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Iduser,UserName,PassWord,Email,Avatar,Idrole,IsActive,Contact")] User user)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count() > 0 && files[0].Length > 0)
                {
                    var file = files[0];
                    var FileName = file.FileName;
                    // upload ảnh vào thư mục wwwroot\\images\\Category var path = Path.Combine(Directory.GetCurrentDirectory(),
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\user", FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                        user.Avatar = "/images/user/" + FileName; // gán tên
                    }

                }


                // account.CreatedDate = DateTime.Now;
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View(user);
        }

        // GET: Admins/Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["Idrole"] = new SelectList(_context.Roles, "Idrole", "Idrole", user.Idrole);
            return View(user);
        }

        // POST: Admins/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Iduser,UserName,PassWord,Email,Avatar,Idrole,IsActive,Contact")] User user)
        {
            if (id != user.Iduser)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Iduser))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idrole"] = new SelectList(_context.Roles, "Idrole", "Idrole", user.Idrole);
            return View(user);
        }

        // GET: Admins/Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.IdroleNavigation)
                .FirstOrDefaultAsync(m => m.Iduser == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Admins/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'W3studyContext.Users'  is null.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
          return (_context.Users?.Any(e => e.Iduser == id)).GetValueOrDefault();
        }
    }
}
