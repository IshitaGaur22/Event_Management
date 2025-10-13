using Event_Management.Exceptions;
using Event_Management.Models;
using Event_Management.Repository;


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
                var result = repository.UpdateEventName(id, newName);
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


        public int UpdateEventDescription(int id, string description)
        {
            try
            {
                var result=repository.UpdateEventDescription(id,description);
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

        public int UpdateEventDate(int id, DateOnly date)
        {
            try
            {
                var result = repository.UpdateEventDate(id,date);
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

        public int UpdateEventTime(int id, TimeOnly time)
        {
            try
            {
                var result = repository.UpdateEventTime(id,time);
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

        public int UpdateEventLocation(int id, string location)
        {
            try
            {
                var result = repository.UpdateEventLocation(id,location);
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
        public IEnumerable<Event> GetAllEvents() => repository.GetAllEvents();


    }
}


