using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EduHome.ViewComponents
{
    public class BannerAreaViewComponent : ViewComponent
    {
        private readonly IBackgroundImagesService _backgroundImagesService;

        public BannerAreaViewComponent(IBackgroundImagesService backgroundImagesService)
        {
            _backgroundImagesService = backgroundImagesService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string head)
        {
            ViewData["Head"] = head;
            Dictionary<string, string> image = _backgroundImagesService.GetBackgroundImages();
            return (await Task.FromResult(View(image)));
        }
    }
}
