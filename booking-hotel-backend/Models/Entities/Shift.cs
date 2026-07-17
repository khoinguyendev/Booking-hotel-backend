using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace booking_hotel_backend.Models.Entities;

[Table("shifts")]
public class Shift
{
    [Key]
    public long Id { get; set; }

    [Required]
    [Column("hotel_id")]
    public long HotelId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public TimeOnly StartTime { get; set; }

    [Required]
    public TimeOnly EndTime { get; set; }

    public int BreakMinutes { get; set; } = 0;

    public bool Status { get; set; } = true;

    [ForeignKey(nameof(HotelId))]
    public Hotel Hotel { get; set; } = null!;

}