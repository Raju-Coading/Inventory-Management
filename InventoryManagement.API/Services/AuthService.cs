using InventoryManagement.API.Data;
using InventoryManagement.API.DTOs;
using InventoryManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtService _jwtService;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            ApplicationDbContext context,
            JwtService jwtService,
            ILogger<AuthService> logger)
        {
            _context = context;
            _jwtService = jwtService;
            _logger = logger;
        }

        public async Task<bool> RegisterAsync(RegisterRequest request)
        {
            _logger.LogInformation(
                "Registration attempt for email {Email}.",
                request.Email);

            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (existingUser != null)
            {
                _logger.LogWarning(
                    "Registration failed because email {Email} is already registered.",
                    request.Email);

                return false;
            }

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            _logger.LogInformation(
                "User registered successfully with email {Email}.",
                request.Email);

            return true;
        }

        public async Task<string?> LoginAsync(LoginRequest request)
        {
            _logger.LogInformation(
                "Login attempt for email {Email}.",
                request.Email);

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
            {
                _logger.LogWarning(
                    "Login failed for email {Email}: user not found.",
                    request.Email);

                return null;
            }

            bool passwordValid = BCrypt.Net.BCrypt.Verify(
                request.Password,
                user.PasswordHash);

            if (!passwordValid)
            {
                _logger.LogWarning(
                    "Login failed for email {Email}: invalid password.",
                    request.Email);

                return null;
            }

            var token = _jwtService.GenerateToken(
                user.Id,
                user.Email);

            _logger.LogInformation(
                "User logged in successfully with email {Email}.",
                request.Email);

            return token;
        }
    }
}