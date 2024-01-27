using Domain.Entities.Common;
using System;

namespace Domain.Entities.ViewComponentModel
{
    public class NoticeVC : BaseEntity
    {
        public DateTime Date { get; set; }
        public string Content { get; set; }
    }
}
