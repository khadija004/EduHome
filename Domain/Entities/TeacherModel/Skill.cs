using Domain.Entities.Common;
using System.Collections.Generic;

namespace Domain.Entities.TeacherModel
{
    public class Skill : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<TeacherSkill> TeacherSkills { get; set; }
    }
}
