namespace booking_hotel_backend.Models.DTOs.Auth
{
    public class LoginResponse
    {
        public string AccessToken { get; set; } = string.Empty;

        public DateTime ExpiredAt { get; set; }
    }
}