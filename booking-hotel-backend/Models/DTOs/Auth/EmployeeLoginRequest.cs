namespace booking_hotel_backend.Models.DTOs.Auth
{
    public class EmployeeLoginRequest
    {
        public string CodeId { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
