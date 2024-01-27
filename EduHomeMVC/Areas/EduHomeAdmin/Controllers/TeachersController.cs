using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain;
using Domain.Entities.TeacherModel;
using Service.Interfaces;
using EduHome.ViewModels.TeacherVMs;
using Service.Utilities.Helpers;
using Microsoft.AspNetCore.Hosting;
using EduHome.Utilities.Extensions;

namespace EduHome.Areas.EduHomeAdmin.Controllers
{
    [Area("EduHomeAdmin")]
    public class TeachersController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ITeacherService _teacherService;
        private readonly IWebHostEnvironment _env;

        public TeachersController(AppDbContext context,
                        ITeacherService teacherService,
                               IWebHostEnvironment env)
        {
            _context = context;
            _teacherService = teacherService;
            _env = env;
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

        public async Task<IActionResult> Create(TeacherCreateVM teacherCreate)
        {
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Name");
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Name");
            var skills = await _context.Skills.Where(s => !s.IsDeleted).ToListAsync();
            teacherCreate.Skills = skills;
            return View(teacherCreate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public async Task<IActionResult> CreateTake(TeacherCreateVM teacherCreate)
        {
            if (!ModelState.IsValid) return View(teacherCreate);

            Teacher teacher = new Teacher()
            {
                Name = teacherCreate.Name,
                FacultyId = teacherCreate.FacultyId,
                PositionId = teacherCreate.PositionId,
            };
            await _context.Teachers.AddAsync(teacher);
            await _context.SaveChangesAsync();

            var lastTeacher = await _context.Teachers.Where(t => !t.IsDeleted).OrderByDescending(t => t.Id).FirstOrDefaultAsync();

            TeacherSocialMedia teacherSocial = new TeacherSocialMedia()
            {
                TeacherId = lastTeacher.Id,
                Twitter = teacherCreate.Twitter,
                Facebook = teacherCreate.Facebook,
                Skype = teacherCreate.Skype,
                Instagram = teacherCreate.Instagram,
                Pinterest = teacherCreate.Pinterest,
            };
            await _context.TeacherSocialMedias.AddAsync(teacherSocial);

            TeacherContactInfo teacherContact = new TeacherContactInfo()
            {
                TeacherId = lastTeacher.Id,
                Email = teacherCreate.Email,
                PhoneNumber = teacherCreate.PhoneNumber,
            };
            await _context.TeacherContactInfos.AddAsync(teacherContact);

            if (teacherCreate.Photo != null)
            {

                string fileName = Guid.NewGuid().ToString() + "_" + teacherCreate.Photo.FileName;

                string path = Helper.GetFilePath(_env.WebRootPath, "assets/img/teacher", fileName);

                await teacherCreate.Photo.SaveFile(path);

                TeacherDetails teacherDetails = new TeacherDetails()
                {
                    TeacherId = lastTeacher.Id,
                    About = teacherCreate.About,
                    Degree = teacherCreate.Degree,
                    Experience = teacherCreate.Experience,
                    Hobbies = teacherCreate.Hobbies,
                    Image = fileName
                };
                await _context.TeachersDetails.AddAsync(teacherDetails);
            }
            
            for (int i = 0; i < teacherCreate.Skills.Count(); i++)
            {
                var teachskill = new TeacherSkill()
                {
                    TeacherId = lastTeacher.Id,
                    SkillId = teacherCreate.Skills[i].Id,
                    LevelPercentage = teacherCreate.SkillLevels[i]
                };
                await _context.TeacherSkills.AddAsync(teachskill);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id, TeacherCreateVM teacherCreate)
        {
            var teacher = await _teacherService.GetTeacherDetailsById(id);
            if (teacher == null) return NotFound();

            teacherCreate.Email = teacher.TeacherContactInfo.Email;
            teacherCreate.About = teacher.TeacherDetails.About;
            teacherCreate.PhoneNumber = teacher.TeacherContactInfo.PhoneNumber;
            teacherCreate.Facebook = teacher.TeacherSocialMedia.Facebook;
            teacherCreate.Instagram = teacher.TeacherSocialMedia.Instagram;
            teacherCreate.Pinterest = teacher.TeacherSocialMedia.Pinterest;
            teacherCreate.Degree = teacher.TeacherDetails.Degree;
            teacherCreate.Experience = teacher.TeacherDetails.Experience;
            teacherCreate.Hobbies = teacher.TeacherDetails.Hobbies;
            teacherCreate.Skype = teacher.TeacherSocialMedia.Skype;
            teacherCreate.Twitter = teacher.TeacherSocialMedia.Twitter;
            teacherCreate.Image = teacher.TeacherDetails.Image;
            teacherCreate.Name = teacher.Name;

            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Name", teacher.FacultyId);
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Name", teacher.PositionId);
            return View(teacherCreate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        public async Task<IActionResult> EditConfirm(int id, TeacherCreateVM teacherCreate)
        {
            if (id != teacherCreate.Id) return NotFound();
            
            if (ModelState.IsValid)
            {
                try
                {
                    var teacher = await _teacherService.GetTeacherDetailsById(id);

                    teacher.TeacherContactInfo.Email = teacherCreate.Email;
                    teacher.TeacherDetails.About = teacherCreate.About;
                    teacher.TeacherContactInfo.PhoneNumber = teacherCreate.PhoneNumber;
                    teacher.TeacherSocialMedia.Facebook = teacherCreate.Facebook;
                    teacher.TeacherSocialMedia.Instagram = teacherCreate.Instagram;
                    teacher.TeacherSocialMedia.Pinterest = teacherCreate.Pinterest;
                    teacher.TeacherDetails.Degree = teacherCreate.Degree;
                    teacher.TeacherDetails.Experience = teacherCreate.Experience;
                    teacher.TeacherDetails.Hobbies = teacherCreate.Hobbies;
                    teacher.TeacherSocialMedia.Skype = teacherCreate.Skype;
                    teacher.TeacherSocialMedia.Twitter = teacherCreate.Twitter;
                    teacher.Name = teacherCreate.Name;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherExists(teacherCreate.Id))
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

            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Name", teacherCreate.FacultyId);
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Name", teacherCreate.PositionId);
            return View(teacherCreate);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers
                .Include(t => t.Faculty)
                .Include(t => t.Position)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacher = await _teacherService.GetTeacherDetailsById(id);
            teacher.IsDeleted = true;
            teacher.TeacherDetails.IsDeleted = true;
            teacher.TeacherSocialMedia.IsDeleted = true;
            teacher.TeacherContactInfo.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherExists(int id)
        {
            return _context.Teachers.Any(e => e.Id == id);
        }
    }
}
