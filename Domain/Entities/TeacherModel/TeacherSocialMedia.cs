using Domain.Entities.Common;

namespace Domain.Entities.TeacherModel
{
    public class TeacherSocialMedia : BaseEntity
    {
        public string Skype { get; set; }
        public string Facebook { get; set; }
        public string Pinterest { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}
