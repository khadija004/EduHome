using Domain.Entities.Common;
using Domain.Entities.EventModel;
using Service.Utilities.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.ViewModels.EventVMs
{
    public class EventDetailsVM
    {
        public Event Event { get; set; }
        public Paginate<Comment> PaginatedComments { get; set; }
    }
}
