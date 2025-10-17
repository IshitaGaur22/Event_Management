using Event_Management.Services;
using Microsoft.AspNetCore.Mvc;
using Event_Management.Models;

[Route("api/[controller]")]
[ApiController]
public class NotificationController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpGet("User/{userId}")]
    public async Task<ActionResult<IEnumerable<Notification>>> GetUserNotifications(int userId)
    {
        var result = await _notificationService.GetUserNotifications(userId);
        return Ok(result);
    }
}