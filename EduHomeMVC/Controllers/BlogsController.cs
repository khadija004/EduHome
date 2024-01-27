using Domain;
using Domain.Entities.BlogModel;
using Domain.Entities.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.Utilities.Helpers;
using Service.Utilities.Pagination;
using Service.ViewModels.BlogVMs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Controllers
{
    public class BlogsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IBlogService _blogService;
        private readonly ICommentService _commentService;

        public BlogsController(AppDbContext context,
                                  IWebHostEnvironment env,
                                  IBlogService blogService,
                                  ICommentService commentService)
        {
            _context = context;
            _env = env;
            _blogService = blogService;
            _commentService = commentService;
        }
        public async Task<IActionResult> Index(int take = 3, int page = 1)
        {
            ViewData["Take"] = take;
            var paginatedBlog = await _blogService.GetBlogs(null, take, page);
            if (paginatedBlog == null) return NotFound();
            return View(paginatedBlog);
        }

        public async Task<IActionResult> IndexWithSideBar(string searchedBlog, int take = 1, int page = 1)
        {
            ViewData["Take"] = take;
            ViewData["SearchedBlog"] = searchedBlog;
            var paginatedBlog = await _blogService.GetBlogs(searchedBlog, take, page);
            if (paginatedBlog == null) return NotFound();
            return View(paginatedBlog);
        }

        public async Task<IActionResult> Details(int id, int take = 1, int page = 1)
        {
            ViewData["Take"] = take;
            ViewData["Page"] = page;
            if (id == 0) return BadRequest();
            BlogDetailsVM blogDetailsVM = await _blogService.GetBlogById(id, take, page);
            if (blogDetailsVM == null) return BadRequest();
            return View(blogDetailsVM);
        }
    }
}
