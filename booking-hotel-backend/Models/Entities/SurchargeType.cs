using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace booking_hotel_backend.Models.Entities;

[Table("surcharge_types")]
public class SurchargeType
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [MaxLength(255)]
    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    [Column("code")]
    public string Code { get; set; } = string.Empty;

    [Column("description", TypeName = "text")]
    public string? Description { get; set; }

    [Required]
    [Column("status")]
    public SurchargeStatus Status { get; set; } = SurchargeStatus.Active;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


    // Navigation Property
    public virtual ICollection<HotelSurcharge> Surcharges { get; set; } = [];
}