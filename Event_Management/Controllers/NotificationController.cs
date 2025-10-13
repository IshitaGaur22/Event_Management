using Event_Management.Models;
using EventManagement.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Event_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly EventServiceContext _context;

        public NotificationController(EventServiceContext context)
        {
            _context = context;
        }

        // ✅ 1. View all notifications for a particular user
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserNotifications(int userId)
        {
            var notifications = await _context.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();

            return Ok(notifications);
        }

        // ✅ 2. Automatically check status and send notification
        // This can be called by a background service or scheduled job
        [HttpGet("auto-notify")]
        public async Task<IActionResult> AutoNotify()
        {
            var bookings = await _context.Booking
                .Include(b => b.Event)
                .Where(b => b.Status == "Confirmed" || b.Status == "Cancelled")
                .ToListAsync();

            int notificationsSent = 0;

            foreach (var booking in bookings)
            {
                string eventName = booking.Event?.EventName ?? "your event";
                string message = booking.Status == "Confirmed"
                    ? $"Your ticket for event '{eventName}' has been confirmed successfully."
                    : $"Your ticket for event '{eventName}' has been cancelled successfully.";

                bool alreadySent = await _context.Notifications.AnyAsync(n =>
                    n.UserId == booking.UserId &&
                    n.EventId == booking.EventId &&
                    n.Message == message);

                if (!alreadySent)
                {
                    var notification = new Notification
                    {
                        UserId = booking.UserId,
                        EventId = booking.EventId,
                        Message = message,
                        IsRead = false,
                        CreatedAt = DateTime.Now
                    };

                    _context.Notifications.Add(notification);
                    notificationsSent++;
                }
            }

            if (notificationsSent > 0)
                await _context.SaveChangesAsync();

            return Ok($"{notificationsSent} new notifications sent.");
        }
    }
}