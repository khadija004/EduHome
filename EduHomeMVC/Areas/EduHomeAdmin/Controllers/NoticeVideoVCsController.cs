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
    public class NoticeVideoVCsController : Controller
    {
        private readonly AppDbContext _context;

        public NoticeVideoVCsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.NoticeVideoVC.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noticeVideoVC = await _context.NoticeVideoVC
                .FirstOrDefaultAsync(m => m.Id == id);
            if (noticeVideoVC == null)
            {
                return NotFound();
            }

            return View(noticeVideoVC);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VideoUrl,VideoImage")] NoticeVideoVC noticeVideoVC)
        {
            if (ModelState.IsValid)
            {
                _context.Add(noticeVideoVC);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(noticeVideoVC);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noticeVideoVC = await _context.NoticeVideoVC.FindAsync(id);
            if (noticeVideoVC == null)
            {
                return NotFound();
            }
            return View(noticeVideoVC);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VideoUrl,VideoImage")] NoticeVideoVC noticeVideoVC)
        {
            if (id != noticeVideoVC.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(noticeVideoVC);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoticeVideoVCExists(noticeVideoVC.Id))
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
            return View(noticeVideoVC);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noticeVideoVC = await _context.NoticeVideoVC
                .FirstOrDefaultAsync(m => m.Id == id);
            if (noticeVideoVC == null)
            {
                return NotFound();
            }

            return View(noticeVideoVC);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var noticeVideoVC = await _context.NoticeVideoVC.FindAsync(id);
            _context.NoticeVideoVC.Remove(noticeVideoVC);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NoticeVideoVCExists(int id)
        {
            return _context.NoticeVideoVC.Any(e => e.Id == id);
        }
    }
}
