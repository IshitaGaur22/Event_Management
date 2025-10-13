using Event_Management.Models;

namespace Event_Management.Exceptions
{
    public class EventUpdateException:ApplicationException
    {
        public EventUpdateException() { }
        public EventUpdateException(string currentName) :base($"Event with {currentName} event name could not be updated.") { }
    }
}
