using booking_hotel_backend.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace booking_hotel_backend.Models.Entities;

[Table("staff_requests")]
public class StaffRequest
{
    [Key]
    public long Id { get; set; }

    [Required]
    [Column("hotel_staff_id")]
    public long HotelStaffId { get; set; }

    [Required]
    [Column("type")]
    public RequestType Type { get; set; }

    [Required]
    [Column("status")]
    public RequestStatus Status { get; set; } = RequestStatus.Pending;

    [MaxLength(500)]
    [Column("reason")]
    public string? Reason { get; set; }

    [Column("approved_by")]
    public long? ApprovedBy { get; set; }

    [Column("approved_at")]
    public DateTime? ApprovedAt { get; set; }

    [MaxLength(500)]
    [Column("reject_reason")]
    public string? RejectReason { get; set; }

    [Required]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    [ForeignKey(nameof(HotelStaffId))]
    public virtual HotelStaff HotelStaff { get; set; } = null!;

    public virtual LeaveRequest? LeaveRequest { get; set; }

    public virtual ShiftChangeRequest? ShiftChangeRequest { get; set; }

    public virtual OvertimeRequest? OvertimeRequest { get; set; }
}