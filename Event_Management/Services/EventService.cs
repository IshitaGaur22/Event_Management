using Event_Management.Data;
using Event_Management.Exceptions;
using Event_Management.Models;
using Event_Management.Repository;
using Microsoft.EntityFrameworkCore;


namespace Event_Management.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository repository;
        private readonly Event_ManagementContext _context;
        private readonly IEmailService _emailService;

        public EventService(IEventRepository repo, Event_ManagementContext context, IEmailService emailService)
        {
            repository = repo;
            _context = context;
            _emailService = emailService;
        }
        

        

      


        public int CreateEvent(Event ev)
        {
            if (repository.GetEvent(ev.EventName) == 1) 
                throw new EventAlreadyExistsException(ev.EventName);

            try
            {
                return repository.AddEvent(ev);
            }
            catch (Exception ex)
            {
                throw new EventCreationException(ex.Message);
            }
        }

        public Event GetEventbyId(int id)
        {
            var ticket = repository.GetEventbyId(id);
            if (ticket == null)
                throw new TicketNotFoundException(id);
            return ticket;
        }
        public IEnumerable<Event> GetAllTickets() => repository.GetAllTickets();


        public void Delete(string eventName)
        {
            if (repository.GetEvent(eventName) == 0)
                throw new EventsNotFoundException(eventName);
            try
            {
                repository.Delete(eventName);
            }
            catch
            {
                throw new EventDeletionException(eventName);
            }

        }
        public int GetTotalEvents()
        {
            return repository.GetTotalEvents();
        }
        
        public int UpdateEvent(int id, string? name, string? description, DateOnly? date, TimeOnly? time, string? location)
        {
            try
            {
                var result = repository.UpdateEvent(id, name, description, date, time, location);
                if (result == 0)
                    throw new EventUpdateException($"Event with ID {id} not found.");
                return result;
            }
            catch (EventUpdateException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new EventUpdateException($"An error occurred while updating event ID {id}.");
            }
        }



        public Event FetchEventName(string eventName)
        {
            var eventDetails = repository.GetEventByName(eventName);

            if (eventDetails == null)
                throw new EventsNotFoundException(eventName);

            return eventDetails;
        }




        public List<Event> FetchEventLocation(string location)
        {
            var eventDetails = repository.GetEventByLocation(location);
            if (eventDetails == null)
                throw new EventsNotFoundException(location);
            return eventDetails;

        }
        public List<Event> FetchEventDate(DateOnly date)
        {
            var eventDetails = repository.GetEventByDate(date);
            if (eventDetails == null)
                throw new EventsNotFoundException(date);
            return eventDetails;

        }


        public async Task NotifyUsersAboutEventChange(Event eventObj, string changeType)
        {
            var bookings = await _context.Booking
                .Include(b => b.User)
                .Where(b => b.EventId == eventObj.EventID)
                .ToListAsync();

            foreach (var booking in bookings)
            {
                string subject = $"Event {changeType}: {eventObj.EventName}";
                string body = $"Hi {booking.User.UserName},\n\nThe event '{eventObj.EventName}' has been {changeType.ToLower()}.\nDate: {eventObj.EventDate}\nTime: {eventObj.EventTime}\nLocation: {eventObj.Location}\n\nPlease check your booking details.";
                await _emailService.SendEmailAsync(booking.User.Email, subject, body);
            }
        }
        public IEnumerable<Event> GetAllEvents() => repository.GetAllEvents();


    }
}