using Event_Management.DTOs;
using Event_Management.Models;
using Microsoft.AspNetCore.Identity.Data;

namespace Event_Management.Services
{
    public interface IUserService
    {
        Task<string> RegisterAsync(RegisterDto dto);
        Task<string> LoginAsync(LoginDto dto);
        Task<bool> ValidateLoginAsync(LoginDto dto);
        Task ResetPasswordAsync(ResetPasswordDto dto);
    }
}
