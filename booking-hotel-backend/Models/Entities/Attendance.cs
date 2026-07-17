using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace booking_hotel_backend.Models.Entities;

[Table("attendances")]
public class Attendance
{
    [Key]
    public long Id { get; set; }
    [Required]
    [Column("work_schedule_id")]
    public long WorkScheduleId { get; set; }


    public TimeOnly? CheckInTime { get; set; }

    public TimeOnly? CheckOutTime { get; set; }

    public AttendanceStatus Status { get; set; }

    public string? Note { get; set; }

    
    public WorkSchedule WorkSchedule { get; set; } = null!;
}