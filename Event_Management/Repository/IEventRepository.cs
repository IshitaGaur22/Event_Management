using Event_Management.Models;

namespace Event_Management.Repository
{
    public interface IEventRepository
    {
        public List<Event> GetAllEvents();
        public int AddEvent(Event e);
        public Event GetEventByID(int id);
        public int UpdateEvent(int id, Event e);
        public int DeleteEvent(int id);
    }
}
