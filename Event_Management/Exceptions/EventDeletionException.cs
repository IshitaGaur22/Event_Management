namespace Event_Management.Exceptions
{
    public class EventDeletionException:ApplicationException
    {
        public EventDeletionException() { }
        public EventDeletionException(string eventName) : base($"Event with Event Name {eventName} could not be deleted.") { }
    }
}
