using Event_Management.Exceptions;
using Event_Management.Models;
using Event_Management.Services;
using EventManagement.Data;

namespace Event_Management.Repository
{
    public class EventRepository :IEventRepository
    {
        private readonly EventServiceContext _context;
        public EventRepository(EventServiceContext context)
        {
            _context = context;
        }
        public List<Event> GetAllEvents()
        {
            return _context.Event.ToList();
        }
        public int AddEvent(Event e)
        {
            _context.Event.Add(e);
            return _context.SaveChanges();
        }

        public Event GetEventByID(int id)
        {
            return _context.Event.FirstOrDefault(s=>s.EventID==id);
        }

        public int UpdateEvent(int id, Event e)
        {
            var existingEvent = _context.Event.Find(id); // Retrieve the tracked entity
            if (existingEvent == null)
            {
                throw new EventNotFoundException($"Student with student id {id} does not exist");
            }

            // Update properties
            existingEvent.EventName = e.EventName;
            existingEvent.EventDescription = e.EventDescription;
            existingEvent.EventDate = e.EventDate;

            return _context.SaveChanges();
        }
        public int DeleteEvent(int id)
        {
            _context.Event.Remove(GetEventByID(id));
            return _context.SaveChanges();
        }
    }
}
