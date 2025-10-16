using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Event_Management.Models;
using Event_Management.Data;
using Event_Management.Services;
namespace Event_Management.Services
{
   
    public class ReminderService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ReminderService> _logger;

        public ReminderService(IServiceProvider serviceProvider, ILogger<ReminderService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var context = scope.ServiceProvider.GetRequiredService<Event_ManagementContext>();
                        var emailService = scope.ServiceProvider.GetRequiredService<EmailService>();

                        var tomorrow = DateOnly.FromDateTime(DateTime.Today.AddDays(1));

                        var bookings = await context.Booking
                            .Include(b => b.Event)
                            .Include(b => b.User)
                            .Where(b => b.Event.EventDate == tomorrow && b.Status == "Confirmed")
                            .ToListAsync();

                        foreach (var booking in bookings)
                        {
                            var subject = "Event Reminder";
                            var body = $"Hi {booking.User.Email},<br/><br/>" +
                                       $"This is a reminder that your event <strong>{booking.Event.EventName}</strong> is scheduled for <strong>{booking.Event.EventDate}</strong> at <strong>{booking.Event.EventTime}</strong>.<br/>" +
                                       $"Location: {booking.Event.Location}<br/><br/>" +
                                       $"Thank you for using our Event Management System.";

                            await emailService.SendEmailAsync(booking.User.Email, subject, body);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while sending reminder emails.");
                }

                // Wait for 24 hours before running again
                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }
    }
}
