using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace booking_hotel_backend.Models.Entities;
[Table("hotel_brands")]
public class HotelBrand
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [MaxLength(255)]
    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(255)]
    [Column("slug")]
    public string Slug { get; set; } = string.Empty;

    [MaxLength(500)]
    [Column("logo")]
    public string? Logo { get; set; }

    [MaxLength(500)]
    [Column("banner")]
    public string? Banner { get; set; }

    [Column("description", TypeName = "text")]
    public string? Description { get; set; }

    [MaxLength(255)]
    [Column("website")]
    public string? Website { get; set; }

    [MaxLength(20)]
    [Column("phone")]
    public string? Phone { get; set; }

    [MaxLength(255)]
    [EmailAddress]
    [Column("email")]
    public string? Email { get; set; }

    [Required]
    [Column("status")]
    public bool Status { get; set; } = true;

    [Required]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
