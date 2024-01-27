using Domain.Entities.Common;
using Domain.Entities.CourseModel;
using Service.Utilities.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.ViewModels.CourseVMs
{
    public class CourseDetailsVM
    {
        public Course Course { get; set; }
        public Paginate<Comment> PaginatedComments { get; set; }
    }
}
