namespace booking_hotel_backend.Models.DTOs.Shift
{
    public class UpdateShiftRequest
    {
        public long? HotelId { get; set; }
        
        public string? Name { get; set; }

        public TimeOnly? StartTime { get; set; }

        public TimeOnly? EndTime { get; set; }
    }
}
