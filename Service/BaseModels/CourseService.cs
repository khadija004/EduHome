using Domain;
using Domain.Entities.Common;
using Domain.Entities.CourseModel;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using Service.Utilities.Helpers;
using Service.Utilities.Pagination;
using Service.ViewModels.CourseVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.BaseModels
{
    public class CourseService : ICourseService
    {
        private readonly AppDbContext _context;
        public CourseService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Paginate<Course>> GetCourses(string searchedCourse, int take, int page)
        {
            try
            {
                
                    List<int> CourseIds = await _context.Courses.Where(c => !c.IsDeleted &&
                           ((searchedCourse != null) ? c.CourseCategory.Name.Trim().ToLower()
                                          .Contains(searchedCourse.Trim().ToLower()) : true))
                               .OrderByDescending(e => e.Id).Select(e => e.Id).ToListAsync();
                    int after = CourseIds.ElementAtOrDefault(take * (page - 1));
                    int count = CourseIds.Count();
                    List<Course> courses = await _context.Courses
                        .Where(c => !c.IsDeleted && ((searchedCourse != null) ? 
                                        c.CourseCategory.Name.Trim().ToLower()
                                    .Contains(searchedCourse.Trim().ToLower())
                                                        : true) && c.Id <= after)
                        .Include(c => c.Assestment)
                        .Include(c => c.CourseCategory)
                        .Include(c => c.Language)
                        .Include(c => c.CourseImages)
                        .Include(c => c.CourseFeatures)
                        .OrderByDescending(t => t.Id)
                        .ToListAsync();
                    if (take > 0) courses = courses.Take(take).ToList();
                    int totalPage = Helper.GetPageCount(count, take);
                    Paginate<Course> paginatedCourse = new Paginate<Course>(courses, page, totalPage);
                    return paginatedCourse;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CourseDetailsVM> GetCourseById(int id, int take, int page)
        {
            try
            {
                Course course = await _context.Courses
                    .Where(c => c.Id == id)
                    .Include(c => c.Assestment)
                    .Include(c => c.CourseCategory)
                    .Include(c => c.Language)
                    .Include(c => c.CourseImages)
                    .Include(c => c.CourseDetails)
                    .Include(c => c.CourseFeatures)
                    .Include(b => b.Comments)
                    .ThenInclude(c => c.AppUser)
                    .FirstOrDefaultAsync();

                List<int> CommentIds = course.Comments.Where(c => !c.IsDeleted)
                       .OrderByDescending(e => e.Id).Select(e => e.Id).ToList();
                int after = CommentIds.ElementAtOrDefault(take * (page - 1));
                int count = CommentIds.Count();
                List<Comment> Comments = course.Comments
                      .Where(c => c.Id <= after && !c.IsDeleted)
                      .OrderByDescending(c => c.Id)
                      .ToList();
                if (take > 0) Comments = Comments.Take(take).ToList();
                int totalPage = Helper.GetPageCount(count, take);
                Paginate<Comment> paginatedComment = new Paginate<Comment>(Comments, page, totalPage);

                CourseDetailsVM courseDetails = new CourseDetailsVM()
                {
                    Course = course,
                    PaginatedComments = paginatedComment
                };
                return courseDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
