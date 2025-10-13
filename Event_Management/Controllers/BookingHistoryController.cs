using Event_Management.DTOs;
using Event_Management.Models;
using Event_Management.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Event_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingHistoryController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingHistoryController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet("upcoming Events")]
        public IActionResult GetUpcomingBookings([FromQuery] int userId)
        {
            var bookings = _bookingService.GetAllBookings()
                .Where(b => b.UserId == userId && b.Event.EventDate >= DateTime.Today)
                .Select(MapToDto)
                .ToList();

            return Ok(bookings);
        }

        [HttpGet("past Events")]
        public IActionResult GetPastBookings([FromQuery] int userId)
        {
            var bookings = _bookingService.GetAllBookings()
                .Where(b => b.UserId == userId && b.Event.EventDate < DateTime.Today)
                .Select(MapToDto)
                .ToList();

            return Ok(bookings);
        }

        [HttpGet("search By Filter")]
        public IActionResult SearchBookings([FromQuery] int userId, [FromQuery] string eventName, [FromQuery] DateTime? eventDate)
        {
            var bookings = _bookingService.GetAllBookings()
                .Where(b => b.UserId == userId &&
                            (string.IsNullOrEmpty(eventName) || b.Event.EventName.Contains(eventName)) &&
                            (!eventDate.HasValue || b.Event.EventDate.Date == eventDate.Value.Date))
                .Select(MapToDto)
                .ToList();

            return Ok(bookings);
        }

        [HttpPut("{Booking id}/cancel")]
        public IActionResult CancelBooking(int id)
        {
            var booking = _bookingService.GetBookingById(id);
            if (booking == null || booking.Event.EventDate < DateTime.Today)
                return BadRequest("Cannot cancel past bookings.");

            booking.Status = "Cancelled";
            booking.Ticket.TotalSeats += booking.SelectedSeats;

            _bookingService.UpdateBooking(booking);
            return Ok("Booking cancelled and ticket count updated.");
        }

        private BookingHistoryDto MapToDto(Booking b)
        {
            return new BookingHistoryDto
            {
                BookingId = b.BookingId,
                EventName = b.Event.EventName,
                EventDate = b.Event.EventDate,
                Location = b.Event.Location,
                SelectedSeats = b.SelectedSeats,
                PricePerTicket = b.Ticket.PricePerTicket,
                BookingDate = b.BookingDate,
                Status = b.Status
            };
        }
    }
}