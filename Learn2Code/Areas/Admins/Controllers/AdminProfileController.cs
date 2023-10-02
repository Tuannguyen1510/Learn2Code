using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Learn2Code.Models;

namespace Learn2Code.Areas.Admins.Controllers
{
    [Area("Admins")]
    public class AdminProfileController : Controller
    {
        private readonly W3studyContext _context;

        public AdminProfileController(W3studyContext context)
        {
            _context = context;
        }

        // GET: Admins/AdminProfile
        public async Task<IActionResult> Index()
        {
            var w3studyContext = _context.Admins.Include(a => a.IduserNavigation);
            return View(await w3studyContext.ToListAsync());
        }

        // GET: Admins/AdminProfile/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Admins == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins
                .Include(a => a.IduserNavigation)
                .FirstOrDefaultAsync(m => m.Idadmin == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // GET: Admins/AdminProfile/Create
        public IActionResult Create()
        {
            ViewData["Iduser"] = new SelectList(_context.Users, "Iduser", "Iduser");
            return View();
        }

        // POST: Admins/AdminProfile/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idadmin,Iduser")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(admin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Iduser"] = new SelectList(_context.Users, "Iduser", "Iduser", admin.Iduser);
            return View(admin);
        }

        // GET: Admins/AdminProfile/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Admins == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            ViewData["Iduser"] = new SelectList(_context.Users, "Iduser", "Iduser", admin.Iduser);
            return View(admin);
        }

        // POST: Admins/AdminProfile/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idadmin,Iduser")] Admin admin)
        {
            if (id != admin.Idadmin)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(admin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminExists(admin.Idadmin))
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
            ViewData["Iduser"] = new SelectList(_context.Users, "Iduser", "Iduser", admin.Iduser);
            return View(admin);
        }

        // GET: Admins/AdminProfile/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Admins == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins
                .Include(a => a.IduserNavigation)
                .FirstOrDefaultAsync(m => m.Idadmin == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // POST: Admins/AdminProfile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Admins == null)
            {
                return Problem("Entity set 'W3studyContext.Admins'  is null.");
            }
            var admin = await _context.Admins.FindAsync(id);
            if (admin != null)
            {
                _context.Admins.Remove(admin);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminExists(int id)
        {
          return (_context.Admins?.Any(e => e.Idadmin == id)).GetValueOrDefault();
        }
    }
}
