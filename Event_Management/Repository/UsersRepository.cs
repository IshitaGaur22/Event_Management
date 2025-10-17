using Event_Management.Data;
using Event_Management.Models;
using Microsoft.EntityFrameworkCore;

namespace Event_Management.Repository
{
    public class UsersRepository:IUsersRepository
    {
        private readonly Event_ManagementContext _context;

        public UsersRepository(Event_ManagementContext context)
        {
            _context = context;
        }
        public bool Exists(string email, long phone)
        {
            return _context.User.Any(u => u.Email == email);
        }
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.User.ToListAsync();
        }

        public void Register(User us1)
        {
            _context.User.Add(us1);
            _context.SaveChanges();
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            return await _context.User.AnyAsync(u => u.Email == email);
        }

        public async Task AddUserAsync(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.User.Update(user);
            await _context.SaveChangesAsync();
        }

        
    }
}
