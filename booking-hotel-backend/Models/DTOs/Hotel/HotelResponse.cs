using booking_hotel_backend.Models.DTOs.Amenity;

namespace booking_hotel_backend.Models.DTOs.Hotel;

public class HotelResponse
{
    public long Id { get; set; }

    public long BrandId { get; set; }
    public string BrandName { get; set; } = string.Empty;

    public long OwnerId { get; set; }

    public long CityId { get; set; }
    public string City { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Slug { get; set; } = string.Empty;

    public string? Image { get; set; }

    public string? Description { get; set; }

    public string Address { get; set; } = string.Empty;

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public byte Star { get; set; }

    public TimeOnly CheckinTime { get; set; }

    public TimeOnly CheckoutTime { get; set; }

    public bool Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public List<AmenityResponse> Amenities { get; set; } = [];
}