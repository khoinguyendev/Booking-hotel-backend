using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace booking_hotel_backend.Models.Entities;

[Table("work_schedules")]
public class WorkSchedule
{
    [Key]
    public long Id { get; set; }

    [Required]
    [Column("hotel_staff_id")]
    public long HotelStaffId { get; set; }

    [Required]
    [Column("shift_id")]
    public long ShiftId { get; set; }

    [Required]
    public DateOnly WorkDate { get; set; }

    public bool IsDayOff { get; set; }

    [ForeignKey(nameof(HotelStaffId))]
    public HotelStaff HotelStaff { get; set; } = null!;

    [ForeignKey(nameof(ShiftId))]
    public Shift Shift { get; set; } = null!;

    public virtual Attendance? Attendance { get; set; }
}