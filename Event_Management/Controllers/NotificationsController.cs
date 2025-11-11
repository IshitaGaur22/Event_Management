using Event_Management.Hubs;
using Event_Management.Models;
using Event_Management.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class NotificationController : ControllerBase
{
    private readonly INotificationService _notificationService;
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotificationController(INotificationService notificationService, IHubContext<NotificationHub> hubContext)
    {
        _notificationService = notificationService;
        _hubContext = hubContext;
    }

    [HttpGet("User/{userId}")]
    public async Task<ActionResult<IEnumerable<Notification>>> GetUserNotifications(int userId)
    {
        var result = await _notificationService.GetUserNotifications(userId);
        return Ok(result);
    }
}