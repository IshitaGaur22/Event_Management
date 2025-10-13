using Event_Management.Exceptions;
using Event_Management.Models;
using Event_Management.Repository;
using Event_Management.Services;
using NuGet.Versioning;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Event_Management.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository repository;

        public EventService(IEventRepository repo)
        {
            repository = repo;
        }

        public int CreateEvent(Event ev)
        {
            if (repository.GetEventById(ev.EventID) != null)
                throw new EventAlreadyExistsException(ev.EventID);

            try
            {
                return repository.AddEvent(ev);
            }
            catch (Exception ex)
            {
                throw new EventCreationException(ex.Message);
            }  
        }

        public void Delete(string eventName)
        {
            if (repository.GetEvent(eventName) == null)
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
//           if (repository.GetEvent(Event.) == null)
//                throw new EventsNotFoundException(ev.EventName);

//            if (string.IsNullOrWhiteSpace(newName))
//                throw new ArgumentException("New event name cannot be empty.");

//            //ev.EventName = newName;

//            try
//            {
//                return repository.UpdateEventName(newName);
//            }
//            catch (Exception ex)
//            {
//                throw new EventUpdateException(ex.Message);
//            }
        public int UpdateEventName(int id, string newName)
        {

            try
            {
                return repository.UpdateEventName(id,newName);
            }
            catch (Exception ex)
            {
                throw new EventUpdateException(ex.Message);
            }

        }

        public int UpdateEventDescription(int id, string description)
        {
            try
            {
                return repository.UpdateEventDescription(id,description);
            }
            catch (Exception ex)
            {
                throw new EventUpdateException(ex.Message);
            }

        }

        public int UpdateEventDate(int id, DateOnly date)
        {
            try
            {
                return repository.UpdateEventDate(id,date);
            }
            catch (Exception ex)
            {
                throw new EventUpdateException(ex.Message);
            }

        }

        public int UpdateEventTime(int id, TimeOnly time)
        {
            try
            {
                return repository.UpdateEventTime(id,time);
            }
            catch (Exception ex)
            {
                throw new EventUpdateException(ex.Message);
            }

        }

        public int UpdateEventLocation(int id, string location)
        {
            try
            {
                return repository.UpdateEventLocation(id,location);
            }
            catch (Exception ex)
            {
                throw new EventUpdateException(ex.Message);
            }
        }



        public Event FetchEventName(string eventName)
        {
            var eventDetails = repository.GetEvent(eventName);
            if (eventDetails == null)
                throw new EventsNotFoundException(eventName);
            return eventDetails;

        }
        public Event FetchEventLocation(string location)
        {
            var eventDetails = repository.GetEventByLocation(location);
            if (eventDetails == null)
                throw new EventsNotFoundException(location);
            return eventDetails;

        }
        public Event FetchEvenDate(DateOnly date)
        {
            var eventDetails = repository.GetEventByDate(date);
            return eventDetails;

        }
        public IEnumerable<Event> GetAllEvents() => repository.GetAllEvents();


    }
}


