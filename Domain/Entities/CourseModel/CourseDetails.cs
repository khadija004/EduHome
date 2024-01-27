using Domain.Entities.Common;

namespace Domain.Entities.CourseModel
{
    public class CourseDetails : BaseEntity
    {
        public string About { get; set; }
        public string Application { get; set; }
        public string Certification { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
