using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;


namespace Domain.Entities.ViewComponentModel
{
    public class NoticeVideoVC
    {
        public int Id { get; set; }
        public string VideoUrl { get; set; }
        public string VideoImage { get; set; }
        [Required, NotMapped]
        public IFormFile VideoPhoto { get; set; }
    }
}
