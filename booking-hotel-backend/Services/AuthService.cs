using booking_hotel_backend.Common;
using booking_hotel_backend.Common.Exceptions;
using booking_hotel_backend.Data;
using booking_hotel_backend.Extensions;
using booking_hotel_backend.Helpers;
using booking_hotel_backend.Models.DTOs.Auth;
using booking_hotel_backend.Models.Entities;
using booking_hotel_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using static System.Net.WebRequestMethods;

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
                throw new UnauthorizedException(
       ErrorCode.AUTH_001,
       "Email hoặc mật khẩu không đúng.");
            }



            // Nếu dùng BCrypt thì thay bằng:
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                throw new UnauthorizedException(
       ErrorCode.AUTH_001,
       "Email hoặc mật khẩu không đúng.");
            }

            if (!user.IsActive)
            {
                throw new ForbiddenException(
       ErrorCode.AUTH_002,
       "Tài khoản đã bị khóa.");
            }
            if (!user.Verified)
            {
                throw new ForbiddenException(
       ErrorCode.AUTH_002,
       "Tài khoản chưa được kích hoạt.");
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
                ExpiredAt = now.AddHours(8),
                User = user.ToResponse()
            };
        }

        public async Task Register(RegisterRequest request)
        {
            var now = DateTimeExtensions.VietnamNow;

            var exists = await _context.Users
                .AnyAsync(x => x.Email == request.Email);

            if (exists)
            {
                throw new ForbiddenException(
      ErrorCode.AUTH_002,
      "Email đã tồn tại.");
            }
            var otp = Random.Shared.Next(100000, 999999).ToString();
            var user = new User
            {
                Email = request.Email,
                OtpCode = otp,
                ExpiredAt = now.AddMinutes(5),
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Verified = false,
                CodeId=request.Email
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            await _emailService.SendOtpAsync(user.Email, otp);

        }
        public async Task VerifyEmail(VerifyEmailRequest request)
        {
            var now = DateTimeExtensions.VietnamNow;

            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == request.Email);

            if (user == null)
            {
                throw new UnauthorizedException(
       ErrorCode.AUTH_001,
       "Email không tồn tại.");
            }

            if (user.OtpCode != request.Otp|| now > user.ExpiredAt)
            {
                throw new UnauthorizedException(
       ErrorCode.AUTH_001,
       "Mã otp không hợp lệ hoặc đã hết hạn.");
            }


         
            

            user.Verified = true;


            user.ExpiredAt = null;

            await _context.SaveChangesAsync();

        }

        public async Task ResendEmail(ResendEmailRequest request)
        {
            var now = DateTimeExtensions.VietnamNow;

            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == request.Email);

            if (user == null)
            {
                throw new NotFoundException(
                    ErrorCode.USER_001,
                    "Email không tồn tại.");
            }
            if (user.Verified)
            {
                throw new BadRequestException(
                    ErrorCode.AUTH_004,
                    "Tài khoản đã được xác thực.");
            }
            var otp = Random.Shared.Next(100000, 999999).ToString();

            user.OtpCode = otp;
            user.ExpiredAt = now.AddMinutes(5);

            await _context.SaveChangesAsync();

            await _emailService.SendOtpAsync(user.Email, otp);
        }

        public async Task CreateAdmin(string id)
        {
            if (id == "369")
            {
                var exists = await _context.Users
               .AnyAsync(x => x.Email == "Admin@gmail.com");

                if (exists)
                {
                    throw new ForbiddenException(
          ErrorCode.AUTH_002,
          "Email đã tồn tại.");
                }
                var user = new User
                {
                    Email = "Admin@gmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("admin"),
                    Verified = true,
                    CodeId= id
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new BadRequestException(
          ErrorCode.AUTH_002,
          "key khong dung.");
            }

        }
    }
}