using Event_Management.Models;

namespace Event_Management.Services
{
    public interface IEventService
    {
        public List<Event> GetAllEvents();
        public int AddEvent(Event e);
        public int DeleteEvent(int id);
        public int UpdateEvent(int id, Event e);

    }
}
