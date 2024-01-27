using Domain.Entities.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.TeacherModel
{
    public class Teacher : BaseEntity
    {
        [Required(ErrorMessage = "The Name field should not be empty.")]
        [StringLength(20, ErrorMessage = "The Name field length should not be over 20 characters.")]
        public string Name { get; set; }
        [Required]
        //Many Teachers With One Position
        public int PositionId { get; set; }
        public Position Position { get; set; }
        //Many Teachers With One Faculties
        public int FacultyId { get; set; }
        public Faculty Faculty { get; set; }
        //One Teacher With One TeacherDetails
        public TeacherDetails TeacherDetails { get; set; }
        //One Teacher With One TeacherContactInfo
        public TeacherContactInfo TeacherContactInfo { get; set; }
        //One Teacher With One TeacherSocialMedia
        public TeacherSocialMedia TeacherSocialMedia { get; set; }
        //Many Teachers With Many TeacherSkills
        public ICollection<TeacherSkill> TeacherSkills { get; set; }
    }
}
