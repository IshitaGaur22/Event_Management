using BCrypt;
using Event_Management.Auth;
using Event_Management.Data;
using Event_Management.DTOs;
using Event_Management.Models;
using Event_Management.Repository;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using System.Security.Authentication;

namespace Event_Management.Services
{
    public class UserService : IUserService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly ITokenService _tokenService;
        private readonly Event_ManagementContext _context;

        public UserService(IUsersRepository usersRepository, ITokenService tokenService, Event_ManagementContext context)
        {
            _usersRepository = usersRepository;
            _tokenService = tokenService;
            _context = context;
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
        public async Task<UserDetailsDto> GetUserByIdAsync(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
                return null;

            return new UserDetailsDto
            {
                Id = user.UserId,
                Username = user.UserName,
                Location = user.Location,
                PhoneNumber = user.PhoneNumber.ToString(),
                Role = user.Role
            };
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

        public async Task<UserDetailsDto> UpdateUserAsync(int id, UpdateUserDto dto)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            // Only update properties that were provided in the DTO (non-null).
            if (dto.UserName is not null)
                user.UserName = dto.UserName;

            if (dto.PhoneNumber.HasValue)
                user.PhoneNumber = dto.PhoneNumber.Value;

            if (dto.Location is not null)
                user.Location = dto.Location;

            await _usersRepository.UpdateUserAsync(user);

            return new UserDetailsDto
            {
                Id = user.UserId,
                Username = user.UserName,
                Location = user.Location,
                PhoneNumber = user.PhoneNumber.ToString(),
                Role = user.Role
            };
        }
    }
}