using Event_Management.DTOs;
using Event_Management.Models;

namespace Event_Management.Services
{
    public interface IEventService
    {
        public int CreateEvent(Event ev);
        void Delete(string eventName);
        int GetTotalEvents();
        int GetTotalBookings();
        decimal GetTotalRevenue();
        List<EventRevenueDto> GetEventRevenueSummary();
        int GetTotalNoOfUsers();
        public int UpdateEvent(int id, string? name, string? description, string? location, int TotalSeats, decimal PricePerTicket, DateOnly? date, TimeOnly? time, TimeOnly? endTime);
        Event GetEventbyId(int id);
        public Event FetchEventName(string eventName);
        public List<Event> FetchEventLocation(string location);
        public List<Event> FetchEventDate(DateOnly date);
        public IEnumerable<Event> GetAllEvents();
        
    }
}