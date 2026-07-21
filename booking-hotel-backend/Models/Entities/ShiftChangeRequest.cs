using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace booking_hotel_backend.Models.Entities;

[Table("shift_change_requests")]
public class ShiftChangeRequest
{
    [Key]
    public long Id { get; set; }

    [Required]
    [Column("staff_request_id")]
    public long StaffRequestId { get; set; }

    // Lịch làm hiện tại của người gửi đơn
    [Required]
    [Column("work_schedule_id")]
    public long WorkScheduleId { get; set; }

    // Nếu đổi với nhân viên khác thì lưu lịch của người đó
    [Column("target_work_schedule_id")]
    public long? TargetWorkScheduleId { get; set; }

    // Nếu không đổi với ai thì chọn ca mới
    [Column("new_shift_id")]
    public long? NewShiftId { get; set; }

    // Có thể đổi sang ngày khác
    [Column("new_work_date")]
    public DateOnly? NewWorkDate { get; set; }

    [ForeignKey(nameof(StaffRequestId))]
    public virtual StaffRequest StaffRequest { get; set; } = null!;

    [ForeignKey(nameof(WorkScheduleId))]
    public virtual WorkSchedule WorkSchedule { get; set; } = null!;

    [ForeignKey(nameof(TargetWorkScheduleId))]
    public virtual WorkSchedule? TargetWorkSchedule { get; set; }

    [ForeignKey(nameof(NewShiftId))]
    public virtual Shift? NewShift { get; set; }
}