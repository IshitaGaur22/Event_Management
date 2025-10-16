using Event_Management.Models;
using Event_Management.Data;
using Microsoft.EntityFrameworkCore;

namespace Event_Management.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly Event_ManagementContext _context;

        public NotificationRepository(Event_ManagementContext context)
        {
            _context = context;
        }

        public async Task<List<Notification>> GetUserNotifications(int userId)
        {
            return await _context.Notification
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }

        public async Task AddNotification(Notification notification)
        {
            await _context.Notification.AddAsync(notification);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
