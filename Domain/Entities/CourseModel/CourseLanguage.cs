using Domain.Entities.Common;
using System.Collections.Generic;

namespace Domain.Entities.CourseModel
{
    public class CourseLanguage : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
