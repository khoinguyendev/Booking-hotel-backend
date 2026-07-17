namespace booking_hotel_backend.Models.DTOs.Position
{
    public class UpdatePositionRequest
    {
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool Status { get; set; }
    }
}