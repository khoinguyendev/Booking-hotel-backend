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
                FullName = user.FullName,
                Email = user.Email,
                CodeId = user.CodeId,
                Role = user.Role,
                Avatar = user.Avatar,
            };
        }
    }
}
