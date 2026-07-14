namespace booking_hotel_backend.Models.DTOs.Auth
{
    public class VerifyEmailRequest
    {
        public string Email { get; set; }

        public string Otp { get; set; }
    }
    public class ResendEmailRequest
    {
        public string Email { get; set; }

    }
}
