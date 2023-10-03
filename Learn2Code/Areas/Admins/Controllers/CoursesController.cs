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
    public class CoursesController : Controller
    {
        private readonly W3studyContext _context;

        public CoursesController(W3studyContext context)
        {
            _context = context;
        }

        // GET: Admins/Courses
        public async Task<IActionResult> Index(string? name, int page = 1)
        {
            /*
           int limit = 5;


            var a = await _context.Courses.OrderBy(c => c.CourseName).ToPagedListAsync(page, limit);

            if (!String.IsNullOrEmpty(name))
            {
                a = await _context.Courses.Where(c => c.CourseName.Contains(name)).OrderBy(c => c.Idcourse).ToPagedListAsync(page, limit);
            }


            ViewBag.keyword = name;
            */
              var w3studyContext = _context.Courses.Include(c => c.IdcategoryNavigation);
              return View(await w3studyContext.ToListAsync());
             
        }

        // GET: Admins/Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.IdcategoryNavigation)
                .FirstOrDefaultAsync(m => m.Idcourse == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Admins/Courses/Create
        public IActionResult Create()
        {
            ViewData["Idcategory"] = new SelectList(_context.Categorys, "Idcategory", "Idcategory");
            return View();
        }

        // POST: Admins/Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idcourse,Idcategory,CourseDesc,CourseName")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idcategory"] = new SelectList(_context.Categorys, "Idcategory", "Idcategory", course.Idcategory);
            return View(course);
        }

        // GET: Admins/Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["Idcategory"] = new SelectList(_context.Categorys, "Idcategory", "Idcategory", course.Idcategory);
            return View(course);
        }

        // POST: Admins/Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idcourse,Idcategory,CourseDesc,CourseName")] Course course)
        {
            if (id != course.Idcourse)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Idcourse))
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
            ViewData["Idcategory"] = new SelectList(_context.Categorys, "Idcategory", "Idcategory", course.Idcategory);
            return View(course);
        }

        // GET: Admins/Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.IdcategoryNavigation)
                .FirstOrDefaultAsync(m => m.Idcourse == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Admins/Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Courses == null)
            {
                return Problem("Entity set 'W3studyContext.Courses'  is null.");
            }
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
          return _context.Courses.Any(e => e.Idcourse == id);
        }
    }
}
