using Domain.Entities.EventModel;
using System.Collections.Generic;

namespace EduHome.ViewModels.EventVMs
{
    public class EventVM
    {
        public Event Event { get; set; }
        public List<EventSpeaker> EventSpeakers { get; set; }
    }
}
