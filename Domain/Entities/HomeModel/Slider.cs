using Domain.Entities.Common;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.HomeModel
{
    public class Slider : BaseEntity
    {
        public string? Header { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        [Required, NotMapped]
        public IFormFile? Photo { get; set; }
    }
}
