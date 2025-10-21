using Event_Management.Models;

namespace Event_Management.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
