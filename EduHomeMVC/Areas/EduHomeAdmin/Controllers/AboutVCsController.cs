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
    public class AboutVCsController : Controller
    {
        private readonly AppDbContext _context;

        public AboutVCsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.AboutVC.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aboutVC = await _context.AboutVC
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aboutVC == null)
            {
                return NotFound();
            }

            return View(aboutVC);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Image")] AboutVC aboutVC)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aboutVC);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aboutVC);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aboutVC = await _context.AboutVC.FindAsync(id);
            if (aboutVC == null)
            {
                return NotFound();
            }
            return View(aboutVC);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Image")] AboutVC aboutVC)
        {
            if (id != aboutVC.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aboutVC);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AboutVCExists(aboutVC.Id))
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
            return View(aboutVC);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aboutVC = await _context.AboutVC
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aboutVC == null)
            {
                return NotFound();
            }

            return View(aboutVC);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aboutVC = await _context.AboutVC.FindAsync(id);
            _context.AboutVC.Remove(aboutVC);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AboutVCExists(int id)
        {
            return _context.AboutVC.Any(e => e.Id == id);
        }
    }
}
