using Domain.Entities.EventModel;
using Service.Utilities.Pagination;
using Service.ViewModels.EventVMs;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IEventService
    {
        Task<Paginate<Event>> GetEvents(string searchedEvent, int take, int page, bool upcoming);
        Task<EventDetailsVM> GetEventById(int id, int take, int page);
    }
}
