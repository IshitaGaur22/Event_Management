using Event_Management.Models;

namespace Event_Management.Auth
{
    public interface ITokenService
    {
        

            string CreateToken(User us);

        

    }
}
