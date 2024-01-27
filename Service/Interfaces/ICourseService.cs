using Domain.Entities.CourseModel;
using Service.Utilities.Pagination;
using Service.ViewModels.BlogVMs;
using Service.ViewModels.CourseVMs;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ICourseService
    {
        Task<Paginate<Course>> GetCourses(string searchedCourse, int take, int page);
        Task<CourseDetailsVM> GetCourseById(int id, int take, int page);
    }
}
