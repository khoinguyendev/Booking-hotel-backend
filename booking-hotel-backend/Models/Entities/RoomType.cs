using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace booking_hotel_backend.Models.Entities;

[Table("room_types")]
public class RoomType
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [Column("hotel_id")]
    public long HotelId { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Column("max_guest")]
    public int MaxGuest { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("bed_type")]
    public string BedType { get; set; } = string.Empty;

    [Column("room_size", TypeName = "decimal(6,2)")]
    public decimal RoomSize { get; set; }

    [Column("description", TypeName = "text")]
    public string? Description { get; set; }

    // Navigation Properties
    [ForeignKey(nameof(HotelId))]
    public virtual Hotel Hotel { get; set; } = null!;
    public virtual ICollection<Room> Rooms { get; set; } = [];
}