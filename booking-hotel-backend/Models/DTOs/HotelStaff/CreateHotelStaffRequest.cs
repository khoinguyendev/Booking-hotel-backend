using booking_hotel_backend.Models.Enums;
namespace booking_hotel_backend.Models.DTOs.HotelStaff
{
    public class CreateHotelStaffRequest
    {
        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string? Phone { get; set; }

        public long HotelId { get; set; }

        public long PositionId { get; set; }

        public Role Role { get; set; }
        public string CodeId { get; set; } = string.Empty;
    }
}
