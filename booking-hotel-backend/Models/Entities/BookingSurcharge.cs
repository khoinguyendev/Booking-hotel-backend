using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace booking_hotel_backend.Models.Entities;

[Table("booking_surcharges")]
public class BookingSurcharge
{
    [Key]
    [Column("id")]
    public long Id { get; set; }


    [Required]
    [Column("booking_id")]
    public long BookingId { get; set; }


    [Required]
    [Column("hotel_surcharge_id")]
    public long HotelSurchargeId { get; set; }


    // Số tiền phụ thu thực tế của booking này
    [Required]
    [Column("amount", TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }


    [Required]
    [Column("status")]
    public BookingSurchargeStatus Status { get; set; } = BookingSurchargeStatus.Pending;


    // Thời điểm gửi yêu cầu phụ thu
    [Column("requested_at")]
    public DateTime? RequestedAt { get; set; }


    // Thời điểm khách sạn duyệt
    [Column("approved_at")]
    public DateTime? ApprovedAt { get; set; }


    // Người duyệt (nhân viên khách sạn)
    [Column("approved_by")]
    public int? ApprovedBy { get; set; }


    [Column("note", TypeName = "text")]
    public string? Note { get; set; }


    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


    // Navigation Properties

    [ForeignKey(nameof(BookingId))]
    public virtual Booking Booking { get; set; } = null!;


    [ForeignKey(nameof(HotelSurchargeId))]
    public virtual HotelSurcharge HotelSurcharge { get; set; } = null!;


    [ForeignKey(nameof(ApprovedBy))]
    public virtual User? ApprovedUser { get; set; }
}