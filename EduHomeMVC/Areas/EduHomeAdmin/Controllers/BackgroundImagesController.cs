using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain;
using Domain.Entities.Common;

namespace EduHome.Areas.EduHomeAdmin.Controllers
{
    [Area("EduHomeAdmin")]
    public class BackgroundImagesController : Controller
    {
        private readonly AppDbContext _context;

        public BackgroundImagesController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.BackgroundImages.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var backgroundImages = await _context.BackgroundImages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (backgroundImages == null)
            {
                return NotFound();
            }

            return View(backgroundImages);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Key,Value")] BackgroundImages backgroundImages)
        {
            if (ModelState.IsValid)
            {
                _context.Add(backgroundImages);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(backgroundImages);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var backgroundImages = await _context.BackgroundImages.FindAsync(id);
            if (backgroundImages == null)
            {
                return NotFound();
            }
            return View(backgroundImages);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Key,Value")] BackgroundImages backgroundImages)
        {
            if (id != backgroundImages.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(backgroundImages);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BackgroundImagesExists(backgroundImages.Id))
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
            return View(backgroundImages);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var backgroundImages = await _context.BackgroundImages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (backgroundImages == null)
            {
                return NotFound();
            }
            return View(backgroundImages);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var backgroundImages = await _context.BackgroundImages.FindAsync(id);
            _context.BackgroundImages.Remove(backgroundImages);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BackgroundImagesExists(int id)
        {
            return _context.BackgroundImages.Any(e => e.Id == id);
        }
    }
}
