using System;
using System.Collections.Generic;

namespace EduHome.ViewModels.CourseVMs
{
    public class CourseVM
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public ICollection<string> Images { get; set; }
        public string About { get; set; }
        public string Application { get; set; }
        public string Certification { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public int ClassDuration { get; set; }
        public string SkillLevel { get; set; }
        public string Language { get; set; }
        public int Capacity { get; set; }
        public string Assesment { get; set; }
        public decimal Fee { get; set; }

    }
}
