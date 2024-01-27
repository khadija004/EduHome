using Domain;
using Domain.Entities.TeacherModel;
using EduHome.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Service.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Controllers
{
    public class TeachersController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly ITeacherService _teacherService;
        public TeachersController(AppDbContext context,
                                  IWebHostEnvironment env,
                                  ITeacherService teacherService)
        {
            _context = context;
            _env = env;
            _teacherService = teacherService;
        }

        public async Task<IActionResult> Index(int take = 3, int page = 1)
        {
            ViewData["Take"] = take;
            var paginatedTeacher = await _teacherService.GetTeachers(take, page);
            if (paginatedTeacher == null) return NotFound();
            return View(paginatedTeacher);
        }
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0) return NotFound();
            var teacher = await _teacherService.GetTeacherDetailsById(id);
            if (teacher is null) return NotFound();
            return View(teacher);
        }

        public async Task<IActionResult> Basket(int? id)
        {
            if(id==null )
                return NotFound();

            Teacher teachers = await _context.Teachers.FirstOrDefaultAsync(x => x.Id == id);

            List<BasketVM> basket;
            if (Request.Cookies["basket"] != null)
            {
                basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);      
            }
            else
            {
                basket = new List<BasketVM>();
            }

            var existTeacher = basket.FirstOrDefault(t => t.Id == teachers.Id);
            if (existTeacher!=null)
            {
                existTeacher.Count++;
            }
            else
            {
                basket.Add(new BasketVM
                {
                    Id = teachers.Id,
                    Name = teachers.Name,
                    Count = 1
                }
                );
            }

            Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));

            return RedirectToAction("Test123");
        }

        public async Task<IActionResult> Test123()
        {

            return View();
        }
    }
}
