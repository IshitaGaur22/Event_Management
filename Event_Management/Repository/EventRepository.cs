using Microsoft.EntityFrameworkCore;
using Event_Management.Data;
using Event_Management.Exceptions;
using Event_Management.Models;
using System.Linq;
using System.Xml.Linq;

namespace Event_Management.Repository
{

    public class EventRepository : IEventRepository
    {
        private readonly Event_ManagementContext context;

        public EventRepository(Event_ManagementContext ctx)
        {
            context = ctx;
        }

        public int AddEvent(Event ev)
        {
            var evt = context.Event.FirstOrDefault(e => e.EventName == ev.EventName);
            if (evt != null)
            {
                return 0;
            }
            context.Event.Add(ev);
            return context.SaveChanges();
        }
        public int GetTotalEvents()
        {
            return context.Event.Count();
        }

        public Event GetEventbyId(int ticketId)
        {
            var evt = context.Event.FirstOrDefault(t => t.EventID == ticketId);
            if (evt == null)
                throw new TicketNotFoundException(ticketId);
            return evt;
        }

        public IEnumerable<Event> GetAllTickets() => context.Event.ToList();

        public void Delete(string eventName)
        {
            var evt = context.Event.FirstOrDefault(e => e.EventName == eventName);


            context.Event.Remove(evt);
            context.SaveChanges();
        }

        public int UpdateEvent(int id, string? name, string? description, DateOnly? date, TimeOnly? time, string? location)
        {
            var evt = context.Event.FirstOrDefault(e => e.EventID == id);
            if (evt == null)
                return 0;

            if (!string.IsNullOrWhiteSpace(name))
                evt.EventName = name;

            if (!string.IsNullOrWhiteSpace(description))
                evt.Description = description;

            if (date.HasValue)
                evt.EventDate = date.Value;

            if (time.HasValue)
                evt.EventTime = time.Value;

            if (!string.IsNullOrWhiteSpace(location))
                evt.Location = location;

            context.Event.Update(evt);
            return context.SaveChanges();
        }

        public Event GetEventByName(string eventName) =>
    context.Event.Single(e =>
        e.EventName==eventName);

        public List<Event> GetEventById(int id) => context.Event
            .Where(e => e.EventID == id)
            .ToList();

        public List<Event> GetEventByLocation(string location) => context.Event
            .Where(e => e.Location == location)
            .ToList();
        public List<Event> GetEventByDate(DateOnly date) => context.Event
            .Where(e => e.EventDate == date)
            .ToList();
        public IEnumerable<Event> GetAllEvents() => context.Event.ToList();
        public int GetEvent(string eventName)
        {
            var evt = context.Event.FirstOrDefault(e => e.EventName == eventName);
            return evt != null ? 1 : 0;
        }


    }


}
