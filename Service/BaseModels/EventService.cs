using Domain;
using Domain.Entities.Common;
using Domain.Entities.EventModel;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using Service.Utilities.Helpers;
using Service.Utilities.Pagination;
using Service.ViewModels.EventVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.BaseModels
{
    public class EventService : IEventService
    {
        private readonly AppDbContext _context;
        public EventService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Paginate<Event>> GetEvents(string searchedEvent, int take, int page, bool upcoming)
        {
            try
            {
                List<int> EventIds = await _context.Events.Where(c => !c.IsDeleted &&
                                    (searchedEvent != null) ? c.Name.Trim().ToLower()
                                    .Contains(searchedEvent.Trim().ToLower()) : true)
                        .OrderByDescending(e => e.Id).Select(e => e.Id).ToListAsync();
                int after = EventIds.ElementAtOrDefault(take * (page - 1));
                var count = EventIds.Count();
                List<Event> events = await _context.Events
                      .Where(c => !c.IsDeleted && ((searchedEvent != null) ?
                                                    c.Name.Trim().ToLower()
                                   .Contains(searchedEvent.Trim().ToLower())
                                                     : true) && c.Id <= after)
                      .OrderByDescending(t => t.Id)
                      .ToListAsync();
                if (upcoming == true) events = events.Where(e => e.Date.CompareTo(DateTime.Now) > 0).ToList();
                if (take > 0) events = events.Take(take).ToList();
                int totalPage = Helper.GetPageCount(count, take);
                Paginate<Event> paginatedEvent = new Paginate<Event>(events, page, totalPage);
                return paginatedEvent;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<EventDetailsVM> GetEventById(int id, int take, int page)
        {
            try
            {
                Event eventt = await _context.Events
                    .Where(t => t.Id == id)
                    .Include(t => t.EventSpeakers)
                    .ThenInclude(t => t.Speaker)
                    .Include(e => e.Comments)
                    .ThenInclude(c => c.AppUser)
                    .FirstOrDefaultAsync();

                List<int> CommentIds = eventt.Comments.Where(c => !c.IsDeleted)
                       .OrderByDescending(e => e.Id).Select(e => e.Id).ToList();
                int after = CommentIds.ElementAtOrDefault(take * (page - 1));
                int count = CommentIds.Count();
                List<Comment> Comments = eventt.Comments
                      .Where(c => c.Id <= after && !c.IsDeleted)
                      .OrderByDescending(c => c.Id)
                      .ToList();
                if (take > 0) Comments = Comments.Take(take).ToList();
                int totalPage = Helper.GetPageCount(count, take);
                Paginate<Comment> paginatedComment = new Paginate<Comment>(Comments, page, totalPage);
                EventDetailsVM eventDetails = new EventDetailsVM()
                {
                    Event = eventt,
                    PaginatedComments = paginatedComment
                };
                return eventDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
