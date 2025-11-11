using Event_Management.Repository;
using Event_Management.Services;

public class EventReminderService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public EventReminderService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var bookingRepository = scope.ServiceProvider.GetRequiredService<IBookingRepository>();
                var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();

                var upcomingEvents = bookingRepository.GetEventsStartingSoon(60);

                foreach (var evnt in upcomingEvents)
                {
                    var users = bookingRepository.GetUsersForEvent(evnt.EventID);
                    foreach (var user in users)
                    {
                        await notificationService.SendEventReminder(user, evnt);
                    }
                }
            }

            await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken);
        }
    }
}