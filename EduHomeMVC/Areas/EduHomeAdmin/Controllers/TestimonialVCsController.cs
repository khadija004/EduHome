using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain;
using Domain.Entities.ViewComponentModel;

namespace EduHome.Areas.EduHomeAdmin.Controllers
{
    [Area("EduHomeAdmin")]
    public class TestimonialVCsController : Controller
    {
        private readonly AppDbContext _context;

        public TestimonialVCsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.TestimonialVC.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testimonialVC = await _context.TestimonialVC
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testimonialVC == null)
            {
                return NotFound();
            }

            return View(testimonialVC);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WriterFullname,Position,Text,Image,Id,IsDeleted")] TestimonialVC testimonialVC)
        {
            if (ModelState.IsValid)
            {
                _context.Add(testimonialVC);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(testimonialVC);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testimonialVC = await _context.TestimonialVC.FindAsync(id);
            if (testimonialVC == null)
            {
                return NotFound();
            }
            return View(testimonialVC);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WriterFullname,Position,Text,Image,Id,IsDeleted")] TestimonialVC testimonialVC)
        {
            if (id != testimonialVC.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testimonialVC);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestimonialVCExists(testimonialVC.Id))
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
            return View(testimonialVC);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testimonialVC = await _context.TestimonialVC
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testimonialVC == null)
            {
                return NotFound();
            }

            return View(testimonialVC);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var testimonialVC = await _context.TestimonialVC.FindAsync(id);
            _context.TestimonialVC.Remove(testimonialVC);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestimonialVCExists(int id)
        {
            return _context.TestimonialVC.Any(e => e.Id == id);
        }
    }
}
