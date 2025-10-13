namespace Event_Management.Exceptions
{
    public class EventsNotFoundException: ApplicationException
    {
        public EventsNotFoundException(string eventName)
            : base($"Event with name '{eventName}' was not found.")
        {
        }
    }
}
