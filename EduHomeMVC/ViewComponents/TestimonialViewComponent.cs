using Domain.Entities.ViewComponentModel;
using EduHome.ViewModels.ViewComponentViewModels;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EduHome.ViewComponents
{
    public class TestimonialViewComponent : ViewComponent
    {
        private readonly ITestimonialVCService _testimonialService;
        private readonly IBackgroundImagesService _backgroundImagesService;

        public TestimonialViewComponent(ITestimonialVCService testimonialService,
                                IBackgroundImagesService backgroundImagesService)
        {
            _testimonialService = testimonialService;
            _backgroundImagesService = backgroundImagesService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            Dictionary<string, string> images = _backgroundImagesService.GetBackgroundImages();
            List<TestimonialVC> testimonials = await _testimonialService.GetTestimonials();
            TestimonialVM testimonial = new TestimonialVM()
            {
                Images = images,
                Testimonials = testimonials
            };
            return (await Task.FromResult(View(testimonial)));
        }
    }
}
