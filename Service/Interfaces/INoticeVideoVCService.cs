using Domain.Entities.ViewComponentModel;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface INoticeVideoVCService
    {
        Task<NoticeVideoVC> GetVideoVC();
    }
}
