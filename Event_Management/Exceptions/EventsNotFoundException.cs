namespace Event_Management.Exceptions
{
    public class EventsNotFoundException: ApplicationException
    {
        
        public EventsNotFoundException(string eventName)
            : base($"Event with  '{eventName}' was not found.") { }
        public EventsNotFoundException(DateOnly date)
            : base($"Event with date '{date}' was not found.") { }

    }
}
