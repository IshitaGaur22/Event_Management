using Event_Management.Data;
using Event_Management.DTOs;
using Event_Management.Exceptions;
using Event_Management.Models;
using Microsoft.EntityFrameworkCore;
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


            var categoryExists = context.Category.Any(c => c.CategoryID == ev.CategoryID);
            if (!categoryExists)
            {
                throw new CategoryNotFoundException();
            }


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
        public int GetTotalBookings()
        {
            return context.Booking.Count();
        }

        public decimal GetTotalRevenue()
        {
            return context.Booking
                .Include(b => b.Event)
                .Where(b => b.Event != null)
                .Sum(b => b.SelectedSeats * b.Event.PricePerTicket);
        }

        public int GetTotalNoOfUsers()
        {
            return context.User.Count();
        }

        public Event GetEventbyId(int ticketId)
        {
            var evt = context.Event.FirstOrDefault(t => t.EventID == ticketId);
            if (evt == null)
                throw new TicketNotFoundException(ticketId);
            return evt;
        }

        public List<EventRevenueDto> GetEventRevenueSummary()
        {
            return context.EventRevenueDto
                .FromSqlRaw("EXEC GetEventRevenueSummary")
                .ToList();
        }


        //public IEnumerable<Event> GetAllTickets() => context.Event.ToList();

        //public void Delete(string eventName)
        //{
        //    var evt = context.Event.FirstOrDefault(e => e.EventName == eventName);


        //    context.Event.Remove(evt);
        //    context.SaveChanges();
        //}

        public void Delete(string eventName)
        {
            var evt = context.Event.FirstOrDefault(e => e.EventName == eventName);
            if (evt == null)
                throw new EventsNotFoundException(eventName);

            context.Event.Remove(evt);
            context.SaveChanges();
        }


        public int UpdateEvent(int id, string? name, string? description, string? location, int TotalSeats, decimal PricePerTicket, DateOnly? date, TimeOnly? time, TimeOnly? endTime, string? imagePath)
        {
            var evt = context.Event.FirstOrDefault(e => e.EventID == id);
            if (evt == null)
                return 0;

            if (!string.IsNullOrWhiteSpace(name))
                // FIX: Only throw exception if the name exists on a DIFFERENT event ID
                if (context.Event.Any(e => e.EventName == name && e.EventID != id))
                    throw new EventAlreadyExistsException(name);
                else
                    evt.EventName = name;

            if (!string.IsNullOrWhiteSpace(description))
                evt.Description = description;

            if (date.HasValue)
                evt.EventDate = date.Value;

            if (time.HasValue)
                evt.EventTime = time.Value;

            if (!string.IsNullOrWhiteSpace(location))
                evt.Location = location;

            if (endTime.HasValue)
                evt.EndTime = endTime.Value;

            if (TotalSeats > 0)
                evt.TotalSeats = TotalSeats;
            if (PricePerTicket > 0)
                evt.PricePerTicket = PricePerTicket;

            if (!string.IsNullOrWhiteSpace(imagePath))
                evt.ImagePath = imagePath;


            context.Event.Update(evt);
            return context.SaveChanges();
        }

        public Event GetEventByName(string eventName) =>
    context.Event.SingleOrDefault(e =>
        e.EventName == eventName);

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