using System.ComponentModel.DataAnnotations;

namespace booking_hotel_backend.Models.DTOs.HotelBrand
{
    public class CreateHotelBrandRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Slug { get; set; } = string.Empty;

        public string? Logo { get; set; }

        public string? Banner { get; set; }

        public string? Description { get; set; }

        public string? Website { get; set; }

        public string? Phone { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public bool Status { get; set; } = true;
    }
}
