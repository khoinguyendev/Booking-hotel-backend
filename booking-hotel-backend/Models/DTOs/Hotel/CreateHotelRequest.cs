using System.ComponentModel.DataAnnotations;

namespace booking_hotel_backend.Models.DTOs.Hotel;

public class CreateHotelRequest
{
    [Required]
    public long BrandId { get; set; }

    [Required]
    public string City { get; set; } = string.Empty;

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Slug { get; set; } = string.Empty;

    public string? Image { get; set; }

    public string? Description { get; set; }

    [Required]
    public string Address { get; set; } = string.Empty;

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public byte Star { get; set; }

    public TimeOnly CheckinTime { get; set; }

    public TimeOnly CheckoutTime { get; set; }

    public bool Status { get; set; } = true;

    public List<long> AmenityIds { get; set; } = [];
}