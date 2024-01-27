using Domain;
using Domain.Entities.BlogModel;
using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using Service.Utilities.Helpers;
using Service.Utilities.Pagination;
using Service.ViewModels.BlogVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.BaseModels
{
    public class BlogService : IBlogService
    {
        private readonly AppDbContext _context;
        public BlogService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Paginate<Blog>> GetBlogs(string searchedBlog, int take, int page)
        {
            try
            {
                List<int> BlogIds = await _context.Blogs.Where(c => !c.IsDeleted &&
                                   (searchedBlog != null) ? c.Title.Trim().ToLower()
                                    .Contains(searchedBlog.Trim().ToLower()) : true)
                       .OrderByDescending(e => e.Id).Select(e => e.Id).ToListAsync();
                int after = BlogIds.ElementAtOrDefault(take * (page - 1));
                int count = BlogIds.Count();
                List<Blog> Blogs = await _context.Blogs
                      .Where(c => c.Id <= after && !c.IsDeleted && ((searchedBlog != null) ? c.Title.Trim().ToLower()
                                                                    .Contains(searchedBlog.Trim().ToLower()) : true))
                      .Include(b => b.BlogImages)
                      .Include(b => b.Comments)
                      .OrderByDescending(b => b.Id)
                      .ToListAsync();
                if (take > 0) Blogs = Blogs.Take(take).ToList();
                int totalPage = Helper.GetPageCount(count, take);
                Paginate<Blog> paginatedBlog = new Paginate<Blog>(Blogs, page, totalPage);
                return paginatedBlog;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BlogDetailsVM> GetBlogById(int id, int take, int page)
        {
            try
            {
                Blog blog = await _context.Blogs
                    .Where(b => b.Id == id)
                    .Include(b => b.BlogImages)
                    .Include(b =>b.Comments)
                    .ThenInclude(c => c.AppUser)
                    .FirstOrDefaultAsync();

                List<int> CommentIds = blog.Comments.Where(c => !c.IsDeleted)
                       .OrderByDescending(e => e.Id).Select(e => e.Id).ToList();
                int after = CommentIds.ElementAtOrDefault(take * (page - 1));
                int count = CommentIds.Count();
                List<Comment> Comments = blog.Comments
                      .Where(c => c.Id <= after && !c.IsDeleted)
                      .OrderByDescending(c => c.Id)
                      .ToList();
                if (take > 0) Comments = Comments.Take(take).ToList();
                int totalPage = Helper.GetPageCount(count, take);
                Paginate<Comment> paginatedComment = new Paginate<Comment>(Comments, page, totalPage);
                
                BlogDetailsVM blogDetails = new BlogDetailsVM()
                {
                    Blog = blog,
                    PaginatedComments = paginatedComment
                };
                return blogDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
