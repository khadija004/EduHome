using Domain.Entities.BlogModel;
using Domain.Entities.Common;
using Domain.Entities.CourseModel;
using Domain.Entities.EventModel;
using Domain.Entities.HomeModel;
using EduHome.ViewModels.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.Utilities.Pagination;
using Service.ViewModels.TeacherVMs;
using System.Diagnostics;

namespace EduHome.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly ISliderService _sliderService;
        private readonly ITeacherService _teacherService;
        private readonly ICourseService _courseService;
        private readonly IEventService _eventService;
        private readonly IBlogService _blogService;
        private readonly IBackgroundImagesService _backgroundImagesService;

        public HomeController(ILogger<HomeController> logger,
                            UserManager<AppUser> userManager,
                                ISliderService sliderService,
                              ITeacherService teacherService,
                                ICourseService courseService,
                                  IEventService eventService,
                                    IBlogService blogService,
            IBackgroundImagesService backgroundImagesService)
        {
            _logger = logger;
            _userManager = userManager;
            _sliderService = sliderService;
            _teacherService = teacherService;
            _courseService = courseService;
            _eventService = eventService;
            _blogService = blogService;
            _backgroundImagesService = backgroundImagesService;
        }

        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _sliderService.GetSliders();
            Paginate<TeacherListVM> teachers = await _teacherService.GetTeachers(3, 1);
            Paginate<Course> courses = await _courseService.GetCourses(null, 3, 1);
            Paginate<Event> events = await _eventService.GetEvents(null, 4, 1, true);
            Paginate<Blog> blogs = await _blogService.GetBlogs(null, 3, 1);
            Dictionary<string, string> images = _backgroundImagesService.GetBackgroundImages();
            HomeVM homeVM = new HomeVM()
            {
                Sliders = sliders,
                Teachers = teachers,
                Courses = courses,
                Events = events,
                Blogs = blogs,
                Images = images
            };
            return View(homeVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
