using booking_hotel_backend.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace booking_hotel_backend.Models.Entities;

[Table("leave_requests")]
public class LeaveRequest
{
    [Key]
    public long Id { get; set; }

    [Required]
    [Column("hotel_staff_id")]
    public long HotelStaffId { get; set; }

    [Required]
    public DateOnly FromDate { get; set; }

    [Required]
    public DateOnly ToDate { get; set; }

    [MaxLength(500)]
    public string? Reason { get; set; }

    public LeaveRequestStatus Status { get; set; } = LeaveRequestStatus.Pending;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey(nameof(HotelStaffId))]
    public HotelStaff HotelStaff { get; set; } = null!;
}