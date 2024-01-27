using Domain.Entities.BlogModel;
using Domain.Entities.Common;
using Service.Utilities.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.ViewModels.BlogVMs
{
    public class BlogDetailsVM
    {
        public Blog Blog { get; set; }
        public Paginate<Comment> PaginatedComments { get; set; }
    }
}
