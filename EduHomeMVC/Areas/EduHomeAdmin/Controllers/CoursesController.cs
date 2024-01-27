using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain;
using Domain.Entities.CourseModel;
using Service.Interfaces;

namespace EduHome.Areas.EduHomeAdmin.Controllers
{
    [Area("EduHomeAdmin")]
    public class CoursesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ICourseService _courseService;

        public CoursesController(AppDbContext context,
                         ICourseService courseService)
        {
            _context = context;
            _courseService = courseService;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses
                .Include(c => c.Assestment)
                .Include(c => c.CourseCategory)
                .Include(c => c.Language)
                .Include(c => c.AppUser)
                .Where(c => c.AppUser.UserName == User.Identity.Name)
                .ToListAsync();

            return View(courses);
        }

        public async Task<IActionResult> Details(int id, int take = 1, int page = 1)
        {
            if (id == 0) return BadRequest();
            var course = await _courseService.GetCourseById(id, take, page);
            if (course == null) return BadRequest();
            return View(course);
        }

        public IActionResult Create()
        {
            ViewData["CourseAssestmentId"] = new SelectList(_context.CourseAssestments, "Id", "Name");
            ViewData["CourseCategoryId"] = new SelectList(_context.CourseCategories, "Id", "Name");
            ViewData["CourseLanguageId"] = new SelectList(_context.CourseLanguages, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description,CourseCategoryId,CourseAssestmentId,CourseLanguageId,Id,IsDeleted")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseAssestmentId"] = new SelectList(_context.CourseAssestments, "Id", "Name", course.CourseAssestmentId);
            ViewData["CourseCategoryId"] = new SelectList(_context.CourseCategories, "Id", "Name", course.CourseCategoryId);
            ViewData["CourseLanguageId"] = new SelectList(_context.CourseLanguages, "Id", "Name", course.CourseLanguageId);
            return View(course);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["CourseAssestmentId"] = new SelectList(_context.CourseAssestments, "Id", "Name", course.CourseAssestmentId);
            ViewData["CourseCategoryId"] = new SelectList(_context.CourseCategories, "Id", "Name", course.CourseCategoryId);
            ViewData["CourseLanguageId"] = new SelectList(_context.CourseLanguages, "Id", "Name", course.CourseLanguageId);
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Description,CourseCategoryId,CourseAssestmentId,CourseLanguageId,Id,IsDeleted")] Course course)
        {
            if (id != course.Id)
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
                    if (!CourseExists(course.Id))
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
            ViewData["CourseAssestmentId"] = new SelectList(_context.CourseAssestments, "Id", "Name", course.CourseAssestmentId);
            ViewData["CourseCategoryId"] = new SelectList(_context.CourseCategories, "Id", "Name", course.CourseCategoryId);
            ViewData["CourseLanguageId"] = new SelectList(_context.CourseLanguages, "Id", "Name", course.CourseLanguageId);
            return View(course);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Assestment)
                .Include(c => c.CourseCategory)
                .Include(c => c.Language)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}
