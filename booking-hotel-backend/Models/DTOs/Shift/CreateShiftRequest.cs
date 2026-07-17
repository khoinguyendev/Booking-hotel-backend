using System.ComponentModel.DataAnnotations;

namespace booking_hotel_backend.Models.DTOs.Shift
{
    public class CreateShiftRequest
    {
        public long HotelId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public TimeOnly StartTime { get; set; }

        [Required]
        public TimeOnly EndTime { get; set; }
    }
}
