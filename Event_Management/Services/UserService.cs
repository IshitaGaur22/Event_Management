using BCrypt;
using Event_Management.Auth;
using Event_Management.DTOs;
using Event_Management.Models;
using Event_Management.Repository;
using Microsoft.AspNetCore.Identity.Data;
using Org.BouncyCastle.Crypto.Generators;
using System.Security.Authentication;

namespace Event_Management.Services
{
    public class UserService : IUserService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly ITokenService _tokenService;

        public UserService(IUsersRepository usersRepository, ITokenService tokenService)
        {
            _usersRepository = usersRepository;
            _tokenService = tokenService;
        }

        public async Task<string> RegisterAsync(RegisterDto dto)
        {
            if (_usersRepository.Exists(dto.Email, dto.PhoneNumber))
                return "Email or phone already registered";

            var user = new User
            {
                UserName = dto.UserName,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = dto.Role,
                PhoneNumber = dto.PhoneNumber,
                Location = dto.Location
            };

            await _usersRepository.AddUserAsync(user);
            return "User registered successfully";
        }

       

        public async Task<LoginResponse> LoginAsync(LoginDto dto)
        {
            var user = await _usersRepository.GetUserByEmailAsync(dto.Email);

            if (user == null)
                throw new InvalidCredentialException("Invalid email");

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new InvalidCredentialException("Invalid password");


            var token = _tokenService.CreateToken(user);
            return new LoginResponse
            {
                Token = token,
                Role = user.Role,
                UserId = user.UserId
            };
        }

        public async Task<bool> ValidateLoginAsync(LoginDto dto)
        {
            var user = await _usersRepository.GetUserByEmailAsync(dto.Email);
            return user != null && BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
        }

        public async Task ResetPasswordAsync(ResetPasswordDto dto)
        {
            var user = await _usersRepository.GetUserByEmailAsync(dto.Email);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            await _usersRepository.UpdateUserAsync(user);
        }

        

        public Task<string> RegisterAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
