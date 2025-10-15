using Event_Management.Models;

namespace Event_Management.Services
{
    public interface IEventService
    {
        public int CreateEvent(Event ev);
        void Delete(string eventName);
        int GetTotalEvents();
        public int UpdateEvent(int id, string? name, string? description, DateOnly? date, TimeOnly? time, string? location);
        Event GetEventbyId(int id);
        IEnumerable<Event> GetAllTickets();
        public Event FetchEventName(string eventName);
        public List<Event> FetchEventLocation(string location);
        public List<Event> FetchEventDate(DateOnly date);
        public IEnumerable<Event> GetAllEvents();
    }
}