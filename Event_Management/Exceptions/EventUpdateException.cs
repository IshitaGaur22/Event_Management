using Event_Management.Models;

namespace Event_Management.Exceptions
{
    public class EventUpdateException : ApplicationException
    {
        public EventUpdateException() { }
        public EventUpdateException(string message)
            : base(message) { }
    }
}

