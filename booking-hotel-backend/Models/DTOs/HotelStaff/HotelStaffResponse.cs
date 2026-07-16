namespace booking_hotel_backend.Models.DTOs.HotelStaff
{
    public class HotelStaffResponse
    {
        public long Id { get; set; }

        public int UserId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? Phone { get; set; }

        public string? Avatar { get; set; }

        public string Position { get; set; } = string.Empty;

        public DateTime JoinedAt { get; set; }

        public bool Status { get; set; }
    }
}