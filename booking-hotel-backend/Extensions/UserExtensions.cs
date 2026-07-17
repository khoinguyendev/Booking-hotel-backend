using booking_hotel_backend.Models.DTOs.Auth;
using booking_hotel_backend.Models.Entities;
namespace booking_hotel_backend.Extensions
{
    public static class UserExtensions
    {
        public static UserResponse ToResponse(this User user)
        {
            return new UserResponse
            {
                Id= user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                Avatar = user.Avatar,
            };
        }
    }
}
