using Domain;
using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using Service.Utilities.Helpers;
using Service.Utilities.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Account
{
    public class CommentService : ICommentService
    {
        private readonly AppDbContext _context;
        public CommentService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Paginate<Comment>> GetComments(int take, int page)
        {
            try
            {
                List<int> CommentIds = await _context.Comments.Where(c => !c.IsDeleted)
                       .OrderByDescending(e => e.Id).Select(e => e.Id).ToListAsync();
                int after = CommentIds.ElementAtOrDefault(take * (page - 1));
                int count = CommentIds.Count();
                List<Comment> Comments = await _context.Comments
                      .Where(c => c.Id <= after && !c.IsDeleted)
                      .Include(b => b.AppUser)
                      .Include(b => b.Event)
                      .Include(b => b.Blog)
                      .Include(b => b.Course)
                      .OrderByDescending(b => b.Id)
                      .ToListAsync();
                if (take > 0) Comments = Comments.Take(take).ToList();
                int totalPage = Helper.GetPageCount(count, take);
                Paginate<Comment> paginatedComment = new Paginate<Comment>(Comments, page, totalPage);
                return paginatedComment;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
