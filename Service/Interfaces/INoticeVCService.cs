using Domain.Entities.ViewComponentModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface INoticeVCService
    {
        Task<List<NoticeVC>> GetNotices();
    }
}
