namespace Event_Management.Exceptions
{
    public class EventAlreadyExistsException : ApplicationException
    {
        public EventAlreadyExistsException() { }
        public EventAlreadyExistsException(string message) : base(message) { }
    }
}
