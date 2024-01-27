using Domain.Entities.Common;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.BlogModel
{
    public class BlogImage : BaseEntity
    {
        public string Image { get; set; }
        [Required, NotMapped]
        public IFormFile Photo { get; set; }
        public bool IsMain { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
