using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Identity.Client;

namespace Event_Management.Exceptions
{
    public class EventNotFoundException : ApplicationException
    {
        public EventNotFoundException() { }
            public EventNotFoundException(string message) : base(message) { }
    }
}
