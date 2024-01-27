using Domain;
using Domain.Entities.TeacherModel;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using Service.Statics;
using Service.Utilities.Helpers;
using Service.Utilities.Pagination;
using Service.ViewModels.TeacherVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.BaseModels
{
    public class TeacherService : ITeacherService
    {
        private readonly AppDbContext _context;
        private readonly IStaticProps _staticProps;
        public TeacherService(AppDbContext context,
                          IStaticProps staticProps)
        {
            _context = context;
            _staticProps = staticProps;
        }

        public async Task<Paginate<TeacherListVM>> GetTeachers(int take, int page)
        {
            try
            {
                //List<int> TeacherIds = await _context.Teachers.OrderByDescending(e => e.Id).Select(e => e.Id).ToListAsync();
                var TeacherIds = await _staticProps.GetIds();
                int after = TeacherIds.ElementAtOrDefault(take * (page - 1));
                var count = TeacherIds.Count();
                List<Teacher> teachers = await _context.Teachers
                    .Where(t => t.Id <= after && t.IsDeleted == false)
                    .Include(t => t.TeacherDetails)
                    .Include(t => t.TeacherContactInfo)
                    .Include(t => t.TeacherSocialMedia)
                    .Include(t => t.TeacherSkills)
                    .Include(t => t.Faculty)
                    .Include(t => t.Position)
                    .OrderByDescending(t => t.Id)
                    .ToListAsync();
                if (take > 0) teachers = teachers.Take(take).ToList();
                var teachersVM = GetMapDatas(teachers);
                int totalPage = Helper.GetPageCount(count, take);
                Paginate<TeacherListVM> paginatedTeacher = new Paginate<TeacherListVM>(teachersVM, page, totalPage);
                return paginatedTeacher;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private List<TeacherListVM> GetMapDatas(List<Teacher> teachers)
        {
            List<TeacherListVM> mapDatas = new List<TeacherListVM>();
            foreach (var teacher in teachers)
            {
                TeacherListVM mapData = new TeacherListVM()
                {
                    Id = teacher.Id,
                    Name = teacher.Name,
                    Image = teacher.TeacherDetails.Image,
                    Position = teacher.Position.Name,
                    About = teacher.TeacherDetails.About,
                    Degree = teacher.TeacherDetails.Degree,
                    Experience = teacher.TeacherDetails.Experience,
                    Hobbies = teacher.TeacherDetails.Hobbies,
                    Faculty = teacher.Faculty.Name,
                    Email = teacher.TeacherContactInfo.Email,
                    PhoneNumber = teacher.TeacherContactInfo.PhoneNumber,
                    Skype = teacher.TeacherSocialMedia.Skype,
                    Facebook = teacher.TeacherSocialMedia.Facebook,
                    Pinterest = teacher.TeacherSocialMedia.Pinterest,
                    Instagram = teacher.TeacherSocialMedia.Instagram,
                    Twitter = teacher.TeacherSocialMedia.Twitter
                };
                mapDatas.Add(mapData);
            }
            return mapDatas;
        }
        public async Task<Teacher> GetTeacherDetailsById(int id)
        {
            try
            {
                Teacher teacher = await _context.Teachers
                    .Where(t => t.Id == id)
                    .Include(t => t.TeacherDetails)
                    .Include(t => t.TeacherContactInfo)
                    .Include(t => t.TeacherSocialMedia)
                    .Include(t => t.Faculty)
                    .Include(t => t.Position)
                    .Include(t => t.TeacherSkills)
                    .ThenInclude(t => t.Skill)
                    .FirstOrDefaultAsync();
                return teacher;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
