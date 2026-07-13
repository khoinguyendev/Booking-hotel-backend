using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace booking_hotel_backend.Models.Entities;

[Table("bookings")]
public class Booking
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [Column("room_id")]
    public long RoomId { get; set; }

    [Required]
    [Column("customer_id")]
    public int CustomerId { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("booking_code")]
    public string BookingCode { get; set; } = string.Empty;

    [Required]
    [Column("checkin")]
    public DateTime Checkin { get; set; }

    [Required]
    [Column("checkout")]
    public DateTime Checkout { get; set; }

    [Required]
    [Column("status")]
    public BookingStatus Status { get; set; } = BookingStatus.Pending;

    [Required]
    [Column("total", TypeName = "decimal(18,2)")]
    public decimal Total { get; set; }

    [Required]
    [Column("payment_status")]
    public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Unpaid;

    // Navigation Properties
    [ForeignKey(nameof(RoomId))]
    public virtual Room Room { get; set; } = null!;

    [ForeignKey(nameof(CustomerId))]
    public virtual User Customer { get; set; } = null!;
    public virtual ICollection<BookingSurcharge> BookingSurcharges { get; set; } = [];
}