using Domain.Entities.Common;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.EventModel
{
    public class Speaker : BaseEntity
    {
        public string Name { get; set; }
        public string Occupation { get; set; }
        public string Image { get; set; }
        [Required, NotMapped]
        public IFormFile Photo { get; set; }
        public ICollection<EventSpeaker> EventSpeakers { get; set; }
    }
}
