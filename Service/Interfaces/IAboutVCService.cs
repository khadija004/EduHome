using Domain.Entities.ViewComponentModel;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IAboutVCService
    {
        Task<AboutVC> GetAboutVC();
    }
}
