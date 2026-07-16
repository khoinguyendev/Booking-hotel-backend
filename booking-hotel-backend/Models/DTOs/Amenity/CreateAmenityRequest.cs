using System.ComponentModel.DataAnnotations;

namespace booking_hotel_backend.Models.DTOs.Amenity;

public class CreateAmenityRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;

    public string? Icon { get; set; }
}