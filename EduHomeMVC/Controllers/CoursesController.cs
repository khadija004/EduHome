using Domain;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace EduHome.Controllers
{
    public class CoursesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly ICourseService _courseService;
        public CoursesController(AppDbContext context,
                                  IWebHostEnvironment env,
                                  ICourseService courseService)
        {
            _context = context;
            _env = env;
            _courseService = courseService;
        }
        public async Task<IActionResult> Index(string searchedCourse, int take = 1, int page = 1)
        {
            ViewData["Take"] = take;
            ViewData["SearchedCourse"] = searchedCourse;
            var paginatedCourses = await _courseService.GetCourses(searchedCourse, take, page);
            if (paginatedCourses == null) return NotFound();
            return View(paginatedCourses);
        }
        public async Task<IActionResult> Details(int id, int take = 1, int page = 1)
        {
            if (id == 0) return BadRequest();
            var course = await _courseService.GetCourseById(id, take, page);
            if (course == null) return BadRequest();
            return View(course);
        }
    }
}
