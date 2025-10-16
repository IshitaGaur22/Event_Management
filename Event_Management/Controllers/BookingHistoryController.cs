using Event_Management.DTOs;
using Event_Management.Services;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingHistoryController : ControllerBase
    {
        private readonly IBookingHistoryService _bookingService;

        public BookingHistoryController(IBookingHistoryService bookingService)
        {
            _bookingService = bookingService;
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
                return Ok(new { message = "Booking cancelled successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
