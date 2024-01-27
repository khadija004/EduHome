using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.TeacherModel
{
    public class TeacherSkill
    {
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int SkillId { get; set; }
        public Skill Skill { get; set; }
        [Range(0, 100, ErrorMessage = "Skill level must be between 0 and 100, since it will be shown by percentage.")]
        public int LevelPercentage { get; set; }
    }
}
