using System.ComponentModel.DataAnnotations;

namespace booking_hotel_backend.Models.DTOs.HotelBrand;

public class UpdateHotelBrandRequest
{
    public string? Name { get; set; }

    public string? Slug { get; set; } 

    public string? Logo { get; set; }

    public string? Banner { get; set; }

    public string? Description { get; set; }

    public string? Website { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public bool? Status { get; set; }
}