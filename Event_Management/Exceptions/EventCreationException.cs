namespace Event_Management.Exceptions
{
    public class EventCreationException:ApplicationException
    {
        public EventCreationException() { }
        public EventCreationException(string message):base($"Error occurred while creating event: {message}") { }
    }
}
