using Domain.Entities.BlogModel;
using Domain.Entities.CourseModel;
using Service.Utilities.Pagination;
using System.Collections.Generic;

namespace EduHome.ViewModels.SidebarVMs
{
    public class SidebarVM
    {
        public List<CourseCategory> Categories { get; set; }
        public Paginate<Blog> Blogs { get; set; }
    }
}
