using Domain.Entities.Common;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.CourseModel
{
    public class CourseImages : BaseEntity
    {
        public string Image { get; set; }
        public bool IsMain { get; set; } = false;
        public int CourseId { get; set; }
        public Course Course { get; set; }
        [NotMapped, Required]
        public IFormFile Photo { get; set; }
    }
}
