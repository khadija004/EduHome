using Domain.Entities.Common;
using System.Collections.Generic;

namespace Domain.Entities.CourseModel
{
    public class Course : BaseEntity
    {
        public string Description { get; set; }
        //One Course With One CourseDetails 
        public CourseDetails CourseDetails { get; set; }
        //One Course With One CourseFeatures
        public CourseFeatures CourseFeatures { get; set; }
        //Many Courses With One Category 
        public int CourseCategoryId { get; set; }
        public CourseCategory CourseCategory { get; set; }
        //Many Courses With One User : Configured by Fluent API
        public AppUser AppUser { get; set; }
        //Many Courses With One CourseAssestmen 
        public int CourseAssestmentId { get; set; }
        public CourseAssestment Assestment { get; set; }
        //Many Courses With One CourseLanguage 
        public int CourseLanguageId { get; set; }
        public CourseLanguage Language { get; set; }
        //One Course With Many CourseImages
        public ICollection<CourseImages> CourseImages { get; set; }
        //One Course With Many Comments
        public ICollection<Comment> Comments { get; set; }

    }
}
