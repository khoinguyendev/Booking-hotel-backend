using booking_hotel_backend.Models.DTOs.Auth;

namespace booking_hotel_backend.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse?> EmployeeLogin(EmployeeLoginRequest request);
        Task<bool> Register(RegisterRequest request);
        Task<bool> VerifyEmail(VerifyEmailRequest request);

    }
}