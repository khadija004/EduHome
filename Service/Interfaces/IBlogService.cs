using Domain.Entities.BlogModel;
using Service.Utilities.Pagination;
using Service.ViewModels.BlogVMs;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IBlogService
    {
        Task<Paginate<Blog>> GetBlogs(string searchedBlog, int take, int page);
        Task<BlogDetailsVM> GetBlogById(int id, int take, int page);
    }
}
