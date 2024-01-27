using Domain;
using Domain.Entities.BlogModel;
using Domain.Entities.CourseModel;
using EduHome.ViewModels.SidebarVMs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using Service.Utilities.Pagination;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
        private readonly IBlogService _blogService;

        public SidebarViewComponent(AppDbContext context,
                                  IBlogService blogService)
        {
            _context = context;
            _blogService = blogService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<CourseCategory> categories = await _context.CourseCategories
                    .Where(cc => !cc.IsDeleted)
                    .Include(cc => cc.Courses)
                    .OrderByDescending(cc => cc.Id)
                    .ToListAsync();
            Paginate<Blog> blogs = await _blogService.GetBlogs(null, 3, 1);
            SidebarVM sidebarVM = new SidebarVM()
            {
                Categories = categories,
                Blogs = blogs,
            };
            return (await Task.FromResult(View(sidebarVM)));
        }
    }
}
