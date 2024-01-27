using Domain;
using Domain.Entities.TeacherModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EduHome.ViewModels.TeacherVMs
{
    
    public class TeacherCreateVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PositionId { get; set; }
        public string About { get; set; }
        public string Degree { get; set; }
        public string Experience { get; set; }
        public string Hobbies { get; set; }
        public int FacultyId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Skype { get; set; }
        public string Facebook { get; set; }
        public string Pinterest { get; set; }
        public string Instagram { get; set; }
        public string Twitter { get; set; }
        public List<Skill> Skills { get; set; }
        public List<int> SkillLevels { get; set; }
        public string Image { get; set; }
        public IFormFile Photo { get; set; }
    }
}
