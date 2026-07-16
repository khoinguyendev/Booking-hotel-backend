using booking_hotel_backend.Models.DTOs.Auth;

namespace booking_hotel_backend.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse?> EmployeeLogin(EmployeeLoginRequest request);
        Task Register(RegisterRequest request);
        Task VerifyEmail(VerifyEmailRequest request);

        Task ResendEmail(ResendEmailRequest request);
        Task CreateAdmin(string id);

    }
}