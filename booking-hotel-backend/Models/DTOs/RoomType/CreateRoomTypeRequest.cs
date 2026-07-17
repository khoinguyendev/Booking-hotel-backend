using System.ComponentModel.DataAnnotations;

namespace booking_hotel_backend.Models.DTOs.RoomType;

public class CreateRoomTypeRequest
{
    [Required]
    public long HotelId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public int MaxGuest { get; set; }

    [Required]
    public string BedType { get; set; } = string.Empty;

    public decimal RoomSize { get; set; }

    public string? Description { get; set; }
}