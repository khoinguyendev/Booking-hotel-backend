using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace booking_hotel_backend.Models.Entities;

[Table("overtime_requests")]
public class OvertimeRequest
{
    [Key]
    public long Id { get; set; }

    [Required]
    [Column("staff_request_id")]
    public long StaffRequestId { get; set; }

    [Required]
    [Column("work_date")]
    public DateOnly WorkDate { get; set; }

    [Required]
    [Column("from_time")]
    public TimeOnly FromTime { get; set; }

    [Required]
    [Column("to_time")]
    public TimeOnly ToTime { get; set; }

    [Column("hours")]
    public decimal Hours { get; set; }

    [ForeignKey(nameof(StaffRequestId))]
    public virtual StaffRequest StaffRequest { get; set; } = null!;
}