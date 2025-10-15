namespace Event_Management.Exceptions
{
    public class EventAlreadyExistsException:ApplicationException
    {
        public EventAlreadyExistsException() { }
        public EventAlreadyExistsException(int eventId): base($"Event with ID '{eventId}' already exists.") { }
        public EventAlreadyExistsException(string eventName) : base($"Event with name '{eventName}' already exists.") { }
    }
}