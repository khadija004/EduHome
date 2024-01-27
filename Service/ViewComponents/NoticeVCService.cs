using Domain;
using Domain.Entities.ViewComponentModel;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.ViewComponents
{
    public class NoticeVCService : INoticeVCService
    {
        private readonly AppDbContext _context;
        public NoticeVCService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<NoticeVC>> GetNotices()
        {
            var notices = await _context.NoticeVCs.Where(n => !n.IsDeleted).ToListAsync();
            return notices;
        }
    }
}
