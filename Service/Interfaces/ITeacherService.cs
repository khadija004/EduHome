using Domain.Entities.TeacherModel;
using Service.Utilities.Pagination;
using Service.ViewModels.TeacherVMs;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ITeacherService
    {
        Task<Paginate<TeacherListVM>> GetTeachers(int take, int page);
        Task<Teacher> GetTeacherDetailsById(int id);
    }
}
