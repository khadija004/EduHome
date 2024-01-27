using Domain.Entities.Common;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.ViewComponentModel
{
    public class TestimonialVC : BaseEntity
    {
        public string WriterFullname { get; set; }
        public string Position { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        [Required, NotMapped]
        public IFormFile Photo { get; set; }
    }
}
