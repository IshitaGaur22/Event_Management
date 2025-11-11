using Event_Management.DTOs;
using Event_Management.Hubs;
using Event_Management.Models;
using Event_Management.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Event_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingHistoryController : ControllerBase
    {
        private readonly IBookingHistoryService _bookingService;
        private readonly IHubContext<NotificationHub> _hubContext;

        public BookingHistoryController(IBookingHistoryService bookingService, IHubContext<NotificationHub> hubContext)
        {
            _bookingService = bookingService;
            _hubContext = hubContext;
        }

        [HttpGet("Upcoming/{userId}")]
        public async Task<ActionResult<IEnumerable<BookingHistoryDTO>>> GetUpcomingBookings(int userId)
        {
            var result = await _bookingService.GetUpcomingBookings(userId);
            return Ok(result);
        }

        [HttpGet("Past/{userId}")]
        public async Task<ActionResult<IEnumerable<BookingHistoryDTO>>> GetPastBookings(int userId)
        {
            var result = await _bookingService.GetPastBookings(userId);
            return Ok(result);
        }

        [HttpGet("SearchByEventName")]
        public async Task<ActionResult<IEnumerable<BookingHistoryDTO>>> SearchByEventName(int userId, string eventName)
        {
            var result = await _bookingService.SearchByEventName(userId, eventName);
            return Ok(result);
        }

        [HttpGet("SearchByDate")]
        public async Task<ActionResult<IEnumerable<BookingHistoryDTO>>> SearchByDate(int userId, DateOnly date)
        {
            var result = await _bookingService.SearchByDate(userId, date);
            return Ok(result);
        }

        [HttpPut("Cancel/{bookingId}")]
        public async Task<IActionResult> CancelBooking(int bookingId)
        {
            try
            {
                await _bookingService.CancelBooking(bookingId);

                //Send real-time notification
                await _hubContext.Clients.All.SendAsync("ReceiveNotification", $"Booking ID {bookingId} has been cancelled.");

                return Ok(new { message = "Booking cancelled successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}