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
            var today = DateOnly.FromDateTime(DateTime.Now);

            var bookings = _bookingService.GetAllBookings()
                .Where(b => b.UserId == userId && b.Event.EventDate >= today)
                .Select(MapToDto)
                .ToList();

            return Ok(bookings);
        }

        [HttpGet("past Events")]
        public IActionResult GetPastBookings([FromQuery] int userId)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);

            var bookings = _bookingService.GetAllBookings()
                .Where(b => b.UserId == userId && b.Event.EventDate < today)
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
                            (!eventDate.HasValue || b.Event.EventDate == DateOnly.FromDateTime(eventDate.Value)))
                .Select(MapToDto)
                .ToList();

            return Ok(bookings);
        }

        [HttpPut("{id}/cancel")]
        public IActionResult CancelBooking(int id)
        {
            var booking = _bookingService.GetBookingById(id);
            var today = DateOnly.FromDateTime(DateTime.Now);

            if (booking == null || booking.Event.EventDate < today)
                return BadRequest("Cannot cancel past bookings.");

            booking.Status = "Cancelled";
            booking.Event.TotalSeats += booking.SelectedSeats;

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
                PricePerTicket = b.Event.PricePerTicket,
                BookingDate = DateOnly.FromDateTime(b.BookingDate),
                Status = b.Status
            };
        }
    }
}