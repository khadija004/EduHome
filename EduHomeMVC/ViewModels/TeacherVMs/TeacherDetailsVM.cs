using Domain.Entities.TeacherModel;
using System.Collections.Generic;

namespace EduHome.ViewModels.TeacherVMs
{
    public class TeacherDetailsVM
    {
        public List<TeacherSkill> TeacherSkills { get; set; }
        public Teacher Teacher { get; set; }
    }
}
