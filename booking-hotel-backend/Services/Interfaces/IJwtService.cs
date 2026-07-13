using booking_hotel_backend.Models.Entities;

namespace booking_hotel_backend.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
