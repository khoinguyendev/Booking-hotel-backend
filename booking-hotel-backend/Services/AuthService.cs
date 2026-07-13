using booking_hotel_backend.Data;
using booking_hotel_backend.Helpers;
using booking_hotel_backend.Models.DTOs.Auth;
using booking_hotel_backend.Models.Entities;
using booking_hotel_backend.Models.Enums;
using booking_hotel_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace booking_hotel_backend.Services
{
    public class AuthService : IAuthService
    {
        private readonly IEmailService _emailService;
        private readonly AppDbContext _context;
        private readonly IJwtService _jwtService;

        public AuthService(
            AppDbContext context,
            IJwtService jwtService,
            IEmailService emailService)
        {
            _context = context;
            _jwtService = jwtService;
            _emailService = emailService;
        }

        public async Task<LoginResponse?> EmployeeLogin(EmployeeLoginRequest request)
        {
            var now = DateTimeExtensions.VietnamNow;

            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == request.Email);

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
            if (!user.Verified)
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
                ExpiredAt = now.AddHours(8)
            };
        }

        public async Task<bool> Register(RegisterRequest request)
        {
            var now = DateTimeExtensions.VietnamNow;

            var exists = await _context.Users
                .AnyAsync(x => x.Email == request.Email);

            if (exists)
                return false;
            var otp = Random.Shared.Next(100000, 999999).ToString();
            var user = new User
            {
                Email = request.Email,
                OtpCode = otp,
                ExpiredAt = now.AddMinutes(5),
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Verified = false
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            await _emailService.SendOtpAsync(user.Email, otp);
            return true;
        }
        public async Task<bool> VerifyEmail(VerifyEmailRequest request)
        {
            var now = DateTimeExtensions.VietnamNow;

            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == request.Email);

            if (user == null)
                return false;

            if (user.OtpCode != request.Otp)
                return false;

            if (now > user.ExpiredAt)
                return false;

            user.Verified = true;


            user.ExpiredAt = null;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}