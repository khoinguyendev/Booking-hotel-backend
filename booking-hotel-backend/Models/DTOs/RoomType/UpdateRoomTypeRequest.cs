using System.ComponentModel.DataAnnotations;

namespace booking_hotel_backend.Models.DTOs.RoomType;

public class UpdateRoomTypeRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public int MaxGuest { get; set; }

    [Required]
    public string BedType { get; set; } = string.Empty;

    public decimal RoomSize { get; set; }

    public string? Description { get; set; }
}