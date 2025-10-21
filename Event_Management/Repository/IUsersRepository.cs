using Event_Management.Models;

namespace Event_Management.Repository
{
    public interface IUsersRepository
    {
        bool Exists(string email, long phone);
        void Register(User us1);
        Task<bool> UserExistsAsync(string email);
        Task AddUserAsync(User user);
        Task<User> GetUserByEmailAsync(string email);
        Task UpdateUserAsync(User user);
        Task<List<User>> GetAllUsersAsync();

    }
}
