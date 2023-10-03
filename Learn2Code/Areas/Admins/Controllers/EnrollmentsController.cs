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
    public class EnrollmentsController : Controller
    {
        private readonly W3studyContext _context;

        public EnrollmentsController(W3studyContext context)
        {
            _context = context;
        }

        // GET: Admins/Enrollments
        public async Task<IActionResult> Index()
        {
            var w3studyContext = _context.Enrollments.Include(e => e.IdcourseNavigation).ThenInclude(c => c.IdcategoryNavigation).Include(e => e.IduserNavigation).ThenInclude(u => u.IdroleNavigation);
            return View(await w3studyContext.ToListAsync());
        }

        // GET: Admins/Enrollments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Enrollments == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .Include(e => e.IdcourseNavigation)
                .Include(e => e.IduserNavigation)
                .FirstOrDefaultAsync(m => m.Idenroll == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // GET: Admins/Enrollments/Create
        public IActionResult Create()
        {
            ViewData["Idcourse"] = new SelectList(_context.Courses, "Idcourse", "Idcourse");
            ViewData["Iduser"] = new SelectList(_context.Users, "Iduser", "Iduser");
            return View();
        }

        // POST: Admins/Enrollments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idenroll,Idcourse,Iduser,EnrollDate")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enrollment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idcourse"] = new SelectList(_context.Courses, "Idcourse", "Idcourse", enrollment.Idcourse);
            ViewData["Iduser"] = new SelectList(_context.Users, "Iduser", "Iduser", enrollment.Iduser);
            return View(enrollment);
        }

        // GET: Admins/Enrollments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Enrollments == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            ViewData["Idcourse"] = new SelectList(_context.Courses, "Idcourse", "Idcourse", enrollment.Idcourse);
            ViewData["Iduser"] = new SelectList(_context.Users, "Iduser", "Iduser", enrollment.Iduser);
            return View(enrollment);
        }

        // POST: Admins/Enrollments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idenroll,Idcourse,Iduser,EnrollDate")] Enrollment enrollment)
        {
            if (id != enrollment.Idenroll)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrollment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollmentExists(enrollment.Idenroll))
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
            ViewData["Idcourse"] = new SelectList(_context.Courses, "Idcourse", "Idcourse", enrollment.Idcourse);
            ViewData["Iduser"] = new SelectList(_context.Users, "Iduser", "Iduser", enrollment.Iduser);
            return View(enrollment);
        }

        // GET: Admins/Enrollments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Enrollments == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .Include(e => e.IdcourseNavigation)
                .Include(e => e.IduserNavigation)
                .FirstOrDefaultAsync(m => m.Idenroll == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // POST: Admins/Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Enrollments == null)
            {
                return Problem("Entity set 'W3studyContext.Enrollments'  is null.");
            }
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment != null)
            {
                _context.Enrollments.Remove(enrollment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnrollmentExists(int id)
        {
          return _context.Enrollments.Any(e => e.Idenroll == id);
        }
    }
}
