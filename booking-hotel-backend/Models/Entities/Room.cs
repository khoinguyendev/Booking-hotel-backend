using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace booking_hotel_backend.Models.Entities;

[Table("rooms")]
public class Room
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [Column("room_type_id")]
    public long RoomTypeId { get; set; }

    [Required]
    [MaxLength(20)]
    [Column("room_number")]
    public string RoomNumber { get; set; } = string.Empty;

    [Column("floor")]
    public int Floor { get; set; }

    [Required]
    [Column("status")]
    public RoomStatus Status { get; set; } = RoomStatus.Available;

    // Navigation Property
    [ForeignKey(nameof(RoomTypeId))]
    public virtual RoomType RoomType { get; set; } = null!;
    public virtual ICollection<Booking> Bookings { get; set; } = [];
}