namespace booking_hotel_backend.Models.DTOs.Position
{
    public class PositionResponse
    {
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}