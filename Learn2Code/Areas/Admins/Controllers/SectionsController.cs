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
    public class SectionsController : Controller
    {
        private readonly W3studyContext _context;

        public SectionsController(W3studyContext context)
        {
            _context = context;
        }

        // GET: Admins/Sections
        public async Task<IActionResult> Index()
        {
            var w3studyContext = _context.Sections.Include(s => s.IdlessionNavigation).ThenInclude(s=> s.IdcourseNavigation).ThenInclude(s=>s.IdcategoryNavigation);
            return View(await w3studyContext.ToListAsync());
        }

        // GET: Admins/Sections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sections == null)
            {
                return NotFound();
            }

            var section = await _context.Sections
                .Include(s => s.IdlessionNavigation)
                .FirstOrDefaultAsync(m => m.Idsection == id);
            if (section == null)
            {
                return NotFound();
            }

            return View(section);
        }

        // GET: Admins/Sections/Create
        public IActionResult Create()
        {
            ViewData["Idlession"] = new SelectList(_context.Lessions, "Idlession", "Idlession");
            return View();
        }

        // POST: Admins/Sections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idsection,Idlession,Titel,TxtContent,Image")] Section section)
        {
            if (ModelState.IsValid)
            {
                _context.Add(section);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idlession"] = new SelectList(_context.Lessions, "Idlession", "Idlession", section.Idlession);
            return View(section);
        }

        // GET: Admins/Sections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sections == null)
            {
                return NotFound();
            }

            var section = await _context.Sections.FindAsync(id);
            if (section == null)
            {
                return NotFound();
            }
            ViewData["Idlession"] = new SelectList(_context.Lessions, "Idlession", "Idlession", section.Idlession);
            return View(section);
        }

        // POST: Admins/Sections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idsection,Idlession,Titel,TxtContent,Image")] Section section)
        {
            if (id != section.Idsection)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(section);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SectionExists(section.Idsection))
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
            ViewData["Idlession"] = new SelectList(_context.Lessions, "Idlession", "Idlession", section.Idlession);
            return View(section);
        }

        // GET: Admins/Sections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sections == null)
            {
                return NotFound();
            }

            var section = await _context.Sections
                .Include(s => s.IdlessionNavigation)
                .FirstOrDefaultAsync(m => m.Idsection == id);
            if (section == null)
            {
                return NotFound();
            }

            return View(section);
        }

        // POST: Admins/Sections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sections == null)
            {
                return Problem("Entity set 'W3studyContext.Sections'  is null.");
            }
            var section = await _context.Sections.FindAsync(id);
            if (section != null)
            {
                _context.Sections.Remove(section);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SectionExists(int id)
        {
          return _context.Sections.Any(e => e.Idsection == id);
        }
    }
}
