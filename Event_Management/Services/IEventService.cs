using Event_Management.Models;

namespace Event_Management.Services
{
    public interface IEventService
    {
        public int CreateEvent(Event ev);
        void Delete(string eventName);
        int UpdateEventName(int id, string newName);
        int UpdateEventDescription(int id, string description);
        int UpdateEventDate(int id, DateOnly date);
        int UpdateEventTime(int id, TimeOnly time);
        int UpdateEventLocation(int id, string location);
        public Event FetchEventName(string eventName);
        public List<Event> FetchEventLocation(string location);
        public List<Event> FetchEventDate(DateOnly date);
        public IEnumerable<Event> GetAllEvents();
    }
}