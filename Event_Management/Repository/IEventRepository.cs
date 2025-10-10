using Event_Management.Models;

namespace Event_Management.Repository
{
    public interface IEventRepository
    {
        public int AddEvent(Event ev);
        public void Delete(string eventName);
        public int UpdateEventName(int id,string newName);
        public int UpdateEventDescription(int id,string description);
        public int UpdateEventDate(int id,DateOnly date);
        public int UpdateEventTime(int id,TimeOnly time);
        public int UpdateEventLocation(int id,string location);
        public Event GetEvent(string eventName);
        public Event GetEventByLocation(string location);
        public Event GetEventByDate(DateOnly date);
        public Event GetEventById(int id);
        public IEnumerable<Event> GetAllEvents();


    }
}


