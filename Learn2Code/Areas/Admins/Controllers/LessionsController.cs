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
    public class LessionsController : Controller
    {
        private readonly W3studyContext _context;

        public LessionsController(W3studyContext context)
        {
            _context = context;
        }

        // GET: Admins/Lessions
        public async Task<IActionResult> Index()
        {
            var w3studyContext = _context.Lessions.Include(l => l.IdcourseNavigation).ThenInclude(l=>l.IdcategoryNavigation);
            return View(await w3studyContext.ToListAsync());
        }

        // GET: Admins/Lessions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Lessions == null)
            {
                return NotFound();
            }

            var lession = await _context.Lessions
                .Include(l => l.IdcourseNavigation)
                .FirstOrDefaultAsync(m => m.Idlession == id);
            if (lession == null)
            {
                return NotFound();
            }

            return View(lession);
        }

        // GET: Admins/Lessions/Create
        public IActionResult Create()
        {
            ViewData["Idcourse"] = new SelectList(_context.Courses, "Idcourse", "Idcourse");
            return View();
        }

        // POST: Admins/Lessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idlession,Idcourse,Titel,LessionDesc,Sort")] Lession lession)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lession);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idcourse"] = new SelectList(_context.Courses, "Idcourse", "Idcourse", lession.Idcourse);
            return View(lession);
        }

        // GET: Admins/Lessions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Lessions == null)
            {
                return NotFound();
            }

            var lession = await _context.Lessions.FindAsync(id);
            if (lession == null)
            {
                return NotFound();
            }
            ViewData["Idcourse"] = new SelectList(_context.Courses, "Idcourse", "Idcourse", lession.Idcourse);
            return View(lession);
        }

        // POST: Admins/Lessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idlession,Idcourse,Titel,LessionDesc,Sort")] Lession lession)
        {
            if (id != lession.Idlession)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lession);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LessionExists(lession.Idlession))
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
            ViewData["Idcourse"] = new SelectList(_context.Courses, "Idcourse", "Idcourse", lession.Idcourse);
            return View(lession);
        }

        // GET: Admins/Lessions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Lessions == null)
            {
                return NotFound();
            }

            var lession = await _context.Lessions
                .Include(l => l.IdcourseNavigation)
                .FirstOrDefaultAsync(m => m.Idlession == id);
            if (lession == null)
            {
                return NotFound();
            }

            return View(lession);
        }

        // POST: Admins/Lessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Lessions == null)
            {
                return Problem("Entity set 'W3studyContext.Lessions'  is null.");
            }
            var lession = await _context.Lessions.FindAsync(id);
            if (lession != null)
            {
                _context.Lessions.Remove(lession);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LessionExists(int id)
        {
          return _context.Lessions.Any(e => e.Idlession == id);
        }
    }
}
