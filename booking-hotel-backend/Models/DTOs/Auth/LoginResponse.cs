using booking_hotel_backend.Models.Enums;

namespace booking_hotel_backend.Models.DTOs.Auth
{
    public class LoginResponse
    {
        public string AccessToken { get; set; } = string.Empty;

        public DateTime ExpiredAt { get; set; }
        public UserResponse User { get; set; } = null!;
    }

    public  class UserResponse
    {
        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? CodeId { get; set; }

        public Role Role { get; set; } 

        public string? Avatar { get; set; }
    }
}