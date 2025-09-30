using Event_Management.Exceptions;
using Event_Management.Models;
using Event_Management.Repository;

namespace Event_Management.Services
{
    public class EventService : IEventService
    {
        public readonly IEventRepository repo;
        public EventService(IEventRepository erepo)
        {
            repo = erepo;
        }

        public List<Event> GetAllEvents()
        {
            return repo.GetAllEvents();
        }

        public int AddEvent(Event e)
        {
            if (repo.GetEventByID(e.EventID) != null)
            {
                throw new EventAlreadyExistsException($"Student with student id {e.EventID} already exists");
            }
            return repo.AddEvent(e);
        }
        public int DeleteEvent(int id)
        {
            if(repo.GetEventByID(id) == null)
            {
                throw new EventNotFoundException($"Student with student id {id} already exists");
            }
            return repo.DeleteEvent(id);
        }

        public int UpdateEvent(int id, Event e)
        {
            if (repo.GetEventByID(id) == null)
            {
                throw new EventAlreadyExistsException($"Student with student id {id} already exists");
            }
            return repo.UpdateEvent(id, e);
        }
    }
}
