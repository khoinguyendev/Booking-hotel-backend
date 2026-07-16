using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace booking_hotel_backend.Models.Entities;

[Table("hotels")]
public class Hotel
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [Column("brand_id")]
    public long BrandId { get; set; }

    [Required]
    [Column("owner_id")]
    public long OwnerId { get; set; }

    [Required]
    [Column("city_id")]
    public long CityId { get; set; }

    [Required]
    [MaxLength(255)]
    [Column("name")]
    public string Name { get; set; } = string.Empty;
    [MaxLength(255)]
    [Column("image")]
    public string Image { get; set; } = string.Empty;
    [Required]
    [MaxLength(255)]
    [Column("slug")]
    public string Slug { get; set; } = string.Empty;

    [Column("description", TypeName = "text")]
    public string? Description { get; set; }

    [Required]
    [MaxLength(500)]
    [Column("address")]
    public string Address { get; set; } = string.Empty;

    [Column("latitude", TypeName = "decimal(10,8)")]
    public decimal? Latitude { get; set; }

    [Column("longitude", TypeName = "decimal(11,8)")]
    public decimal? Longitude { get; set; }

    [MaxLength(20)]
    [Column("phone")]
    public string? Phone { get; set; }

    [MaxLength(255)]
    [EmailAddress]
    [Column("email")]
    public string? Email { get; set; }

    [Column("star")]
    public byte Star { get; set; }
    [Column("city")]
    public string City { get; set; } = null!;

    [Column("checkin_time")]
    public TimeOnly CheckinTime { get; set; }

    [Column("checkout_time")]
    public TimeOnly CheckoutTime { get; set; }

    [Column("status")]
    public bool Status { get; set; } = true;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties

    [ForeignKey(nameof(BrandId))]
    public virtual HotelBrand Brand { get; set; } = null!;

    public virtual ICollection<HotelAmenity> HotelAmenities { get; set; } = [];
    public virtual ICollection<RoomType> RoomTypes { get; set; } = [];
    public virtual ICollection<HotelSurcharge> HotelSurcharges { get; set; } = [];
}
