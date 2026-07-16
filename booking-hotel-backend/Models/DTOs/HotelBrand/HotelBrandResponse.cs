namespace booking_hotel_backend.Models.DTOs.HotelBrand;

public class HotelBrandResponse
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Slug { get; set; } = string.Empty;

    public string? Logo { get; set; }

    public string? Banner { get; set; }

    public string? Description { get; set; }

    public string? Website { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public bool Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}