using Event_Management.Models;
using Event_Management.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class NotificationBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public NotificationBackgroundService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<Event_ManagementContext>();

                var bookings = await context.Booking
                    .Include(b => b.Event)
                    .Where(b => b.Status == "Confirmed" || b.Status == "Cancelled")
                    .ToListAsync();

                foreach (var booking in bookings)
                {
                    string eventName = booking.Event?.EventName ?? "your event";
                    string message = booking.Status == "Confirmed"
                        ? $"Your ticket for event '{eventName}' has been confirmed successfully."
                        : $"Your ticket for event '{eventName}' has been cancelled successfully.";

                    bool alreadySent = await context.Notification.AnyAsync(n =>
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
                        context.Notification.Add(notification);
                    }
                }

                await context.SaveChangesAsync();
            }

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken); // Runs every 5 minutes
        }
    }
}