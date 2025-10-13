using Event_Management.Models;

namespace Event_Management.Exceptions
{
    public class EventUpdateException : ApplicationException
    {
        public EventUpdateException() { }

        public EventUpdateException(int id)
            : base($"Event with event id {id} could not be updated.") { }

        public EventUpdateException(string message)
            : base(message) { }

        //public EventUpdateException(string message, Exception innerException)
        //    : base(message, innerException) { }
    }
}

