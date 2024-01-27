using Domain.Entities.Common;
using System.Collections.Generic;

namespace Domain.Entities.CourseModel
{
    public class CourseCategory : BaseEntity
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public virtual ICollection<CourseCategory> Children { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
