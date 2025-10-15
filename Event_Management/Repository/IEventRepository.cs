using Event_Management.Models;

namespace Event_Management.Repository
{
    public interface IEventRepository
    {
        public int AddEvent(Event ev);
        public void Delete(string eventName);
       public int GetTotalEvents();
        public int UpdateEvent(int id, string? name, string? description, DateOnly? date, TimeOnly? time, string? location);
        public int GetEvent(string eventName);
        public Event GetEventbyId(int ticketId);
        public IEnumerable<Event> GetAllTickets();
        public List<Event> GetEventByLocation(string location);
        public List<Event> GetEventByDate(DateOnly date);
        public List<Event> GetEventById(int id);
        public Event GetEventByName(string eventName);
        public IEnumerable<Event> GetAllEvents();


    }
}