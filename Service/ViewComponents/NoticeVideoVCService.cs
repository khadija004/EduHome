using Domain;
using Domain.Entities.ViewComponentModel;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using System.Threading.Tasks;

namespace Service.ViewComponents
{
    public class NoticeVideoVCService : INoticeVideoVCService
    {
        private readonly AppDbContext _context;
        public NoticeVideoVCService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<NoticeVideoVC> GetVideoVC()
        {
            var noticeVideo = await _context.NoticeVideoVC.FirstOrDefaultAsync();
            return noticeVideo;
        }
    }
}
