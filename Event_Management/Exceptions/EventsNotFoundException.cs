namespace Event_Management.Exceptions
{
    public class EventsNotFoundException: ApplicationException
    {
        private DateOnly date;

        public EventsNotFoundException(string eventName)
            : base($"Event with name '{eventName}' was not found.")
        {
        }

        public EventsNotFoundException(DateOnly date)
        {
            this.date = date;
        }
    }
}
