using Domain.Entities.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities.BlogModel
{
    public class Blog : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public int CommentCount { get; set; }
        //One Course With Many Images
        public ICollection<BlogImage> BlogImages { get; set; }
        //One Course With Many Comments
        public List<Comment> Comments { get; set; }
    }
}
