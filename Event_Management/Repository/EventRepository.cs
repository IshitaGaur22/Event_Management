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
            if (evt == null)
            {
                return 0;
            }
            context.Event.Add(ev);
            return context.SaveChanges();
        }
        public void Delete(string eventName)
        {
            var eventDetails = GetEventByName(eventName);
            if (eventDetails != null)
                context.Event.Remove(eventDetails);
        }
        public int UpdateEventName(int id, string newName)
        {
            var newEvent = context.Event.FirstOrDefault(e => e.EventID == id);
            if (newEvent == null)
            {
                return 0;
            }
            newEvent.EventName = newName;
            context.Event.Update(newEvent);
            return context.SaveChanges();

        }

        public int UpdateEventDescription(int id, string description)
        {
            var evt = context.Event.FirstOrDefault(e => e.EventID == id);
            if (evt == null)
            {
                return 0;
            }
            evt.Description = description;
            context.Event.Update(evt);
            return context.SaveChanges();
        }
        public int UpdateEventDate(int id, DateOnly date)
        {
            var evt = context.Event.FirstOrDefault(e => e.EventID == id);
            if (evt == null)
            {
                return 0;
            }
            evt.EventDate = date;
            context.Event.Update(evt);
            return context.SaveChanges();
        }
        public int UpdateEventTime(int id, TimeOnly time)
        {
            var evt = context.Event.FirstOrDefault(e => e.EventID == id);
            if (evt == null)
            {
                return 0;
            }
            evt.EventTime = time;
            context.Event.Update(evt);
            return context.SaveChanges();
        }
        public int UpdateEventLocation(int id, string location)
        {
            var evt = context.Event.FirstOrDefault(e => e.EventID == id);
            if (evt == null)
            {
                return 0;
            }
            evt.Location = location;
            context.Event.Update(evt);
            return context.SaveChanges();
        }


        public Event GetEventByName(string eventName) => context.Event
           .FirstOrDefault(e => e.EventName.Contains(eventName, StringComparison.OrdinalIgnoreCase));


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
            if (evt == null)
            {
                return 0;
            }
            return 1;
        }

    }
}