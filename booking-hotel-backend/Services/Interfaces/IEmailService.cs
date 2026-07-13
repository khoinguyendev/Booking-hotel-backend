using booking_hotel_backend.Models.DTOs.Auth;

namespace booking_hotel_backend.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendOtpAsync(string email, string otp);
    }
}
