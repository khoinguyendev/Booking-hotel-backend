namespace booking_hotel_backend.Models.DTOs.Position
{
    public class CreatePositionRequest
    {
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool Status { get; set; } = true;
    }
}