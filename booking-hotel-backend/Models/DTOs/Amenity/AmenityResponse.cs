namespace booking_hotel_backend.Models.DTOs.Amenity;

public class AmenityResponse
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Icon { get; set; }
}