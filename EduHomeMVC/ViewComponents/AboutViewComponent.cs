using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System.Threading.Tasks;

namespace EduHome.ViewComponents
{
    public class AboutViewComponent : ViewComponent
    {
        private readonly IAboutVCService _aboutVCService;

        public AboutViewComponent(IAboutVCService aboutVCService)
        {
            _aboutVCService = aboutVCService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var about = await _aboutVCService.GetAboutVC();
            return (await Task.FromResult(View(about)));
        }
    }
}
