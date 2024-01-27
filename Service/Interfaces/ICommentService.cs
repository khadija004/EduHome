using Domain.Entities.Common;
using Service.Utilities.Pagination;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ICommentService
    {
        Task<Paginate<Comment>> GetComments(int take, int page);
    }
}
