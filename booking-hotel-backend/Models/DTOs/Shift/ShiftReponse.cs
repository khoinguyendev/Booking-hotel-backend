

namespace booking_hotel_backend.Models.DTOs.Shift
{
    public class ShiftReponse
    {
        public long Id { get; set; }

        
        public string Name { get; set; } = string.Empty;

        
        public TimeOnly StartTime { get; set; }

        
        public TimeOnly EndTime { get; set; }
    }
}
