using Domain.Entities.BlogModel;
using Domain.Entities.CourseModel;
using Domain.Entities.EventModel;
using Domain.Entities.HomeModel;
using Service.Utilities.Pagination;
using Service.ViewModels.TeacherVMs;
using System.Collections.Generic;

namespace EduHome.ViewModels.Base
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public Paginate<TeacherListVM> Teachers { get; set; }
        public Paginate<Course> Courses { get; set; }
        public Paginate<Event> Events { get; set; }
        public Paginate<Blog> Blogs { get; set; }
        public Dictionary<string, string> Images { get; set; }
    }
}
