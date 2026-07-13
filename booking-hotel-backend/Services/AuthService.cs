using booking_hotel_backend.Data;
using booking_hotel_backend.Models.DTOs.Auth;
using booking_hotel_backend.Models.Entities;
using booking_hotel_backend.Models.Enums;
using booking_hotel_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace booking_hotel_backend.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IJwtService _jwtService;

        public AuthService(
            AppDbContext context,
            IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        public async Task<LoginResponse?> EmployeeLogin(EmployeeLoginRequest request)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.CodeId == request.CodeId);

            if (user == null)
            {
                return null;
            }



            // Nếu dùng BCrypt thì thay bằng:
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return null;
            }

            if (!user.IsActive)
            {
                return null;
            }

            // Chỉ nhân viên/Admin mới được login
            //if (user.Role == Role.Customer)
            //{
            //    return null;
            //}

            var token = _jwtService.GenerateToken(user);

            return new LoginResponse
            {
                AccessToken = token,
                ExpiredAt = DateTime.UtcNow.AddHours(8)
            };
        }

        public async Task<bool> Register(RegisterRequest request)
        {
            var exists = await _context.Users
                .AnyAsync(x => x.Email == request.Email);

            if (exists)
                return false;

            var user = new User
            {
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}