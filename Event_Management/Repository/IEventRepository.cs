using Event_Management.DTOs;
using Event_Management.Models;

namespace Event_Management.Repository
{
    public interface IEventRepository
    {
        public int AddEvent(Event ev);
        public void Delete(string eventName);
        int GetTotalEvents();
        int GetTotalBookings();
        decimal GetTotalRevenue();
        public List<EventRevenueDto> GetEventRevenueSummary();
        int GetTotalNoOfUsers();
        public int UpdateEvent(int id, string? name, string? description, string? location, int TotalSeats, decimal PricePerTicket, DateOnly? date, TimeOnly? time, TimeOnly? endTime);
        public int GetEvent(string eventName);
        public Event GetEventbyId(int ticketId);
        public List<Event> GetEventByLocation(string location);
        public List<Event> GetEventByDate(DateOnly date);
        public List<Event> GetEventById(int id);
        public Event GetEventByName(string eventName);
        public IEnumerable<Event> GetAllEvents();


    }
}