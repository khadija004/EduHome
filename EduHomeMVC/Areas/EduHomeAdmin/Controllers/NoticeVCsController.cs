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
    public class NoticeVCsController : Controller
    {
        private readonly AppDbContext _context;

        public NoticeVCsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.NoticeVCs.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noticeVC = await _context.NoticeVCs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (noticeVC == null)
            {
                return NotFound();
            }

            return View(noticeVC);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,Content,Id,IsDeleted")] NoticeVC noticeVC)
        {
            if (ModelState.IsValid)
            {
                _context.Add(noticeVC);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(noticeVC);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noticeVC = await _context.NoticeVCs.FindAsync(id);
            if (noticeVC == null)
            {
                return NotFound();
            }
            return View(noticeVC);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Date,Content,Id,IsDeleted")] NoticeVC noticeVC)
        {
            if (id != noticeVC.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(noticeVC);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoticeVCExists(noticeVC.Id))
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
            return View(noticeVC);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noticeVC = await _context.NoticeVCs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (noticeVC == null)
            {
                return NotFound();
            }

            return View(noticeVC);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var noticeVC = await _context.NoticeVCs.FindAsync(id);
            _context.NoticeVCs.Remove(noticeVC);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NoticeVCExists(int id)
        {
            return _context.NoticeVCs.Any(e => e.Id == id);
        }
    }
}
