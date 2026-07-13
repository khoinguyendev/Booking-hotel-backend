using System.ComponentModel.DataAnnotations.Schema;

namespace booking_hotel_backend.Models.Entities;

[Table("hotel_amenities")]
public class HotelAmenity
{
    [Column("hotel_id")]
    public long HotelId { get; set; }

    [Column("amenity_id")]
    public long AmenityId { get; set; }

    // Navigation Properties
    [ForeignKey(nameof(HotelId))]
    public virtual Hotel Hotel { get; set; } = null!;

    [ForeignKey(nameof(AmenityId))]
    public virtual Amenity Amenity { get; set; } = null!;
}