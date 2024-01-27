using Domain.Entities.ViewComponentModel;
using System.Collections.Generic;

namespace EduHome.ViewModels.ViewComponentViewModels
{
    public class TestimonialVM
    {
        public List<TestimonialVC> Testimonials { get; set; }
        public Dictionary<string, string> Images { get; set; }
    }
}
