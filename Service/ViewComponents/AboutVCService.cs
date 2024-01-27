using Domain;
using Domain.Entities.ViewComponentModel;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using System.Threading.Tasks;

namespace Service.ViewComponents
{
    public class AboutVCService : IAboutVCService
    {
        private readonly AppDbContext _context;
        public AboutVCService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<AboutVC> GetAboutVC()
        {
            var aboutVC = await _context.AboutVC.FirstOrDefaultAsync();
            return aboutVC;
        }
    }
}
