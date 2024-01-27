using Domain.Entities.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.TeacherModel
{
    public class Position : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public ICollection<Teacher> Teachers { get; set; }
    }
}
