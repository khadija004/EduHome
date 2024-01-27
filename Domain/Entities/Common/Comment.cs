using Domain.Entities.BlogModel;
using Domain.Entities.CourseModel;
using Domain.Entities.EventModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities.Common
{
    public class Comment : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public AppUser AppUser { get; set; }
        public int? EventId { get; set; }
        public Event Event { get; set; }
        public int? BlogId { get; set; }
        public Blog Blog { get; set; }
        public int? CourseId { get; set; }
        public Course Course { get; set; }

    }
}
