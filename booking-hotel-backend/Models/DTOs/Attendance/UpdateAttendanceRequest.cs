using booking_hotel_backend.Models.Entities;

namespace booking_hotel_backend.Models.DTOs.Attendance;

public class UpdateAttendanceRequest
{
    public long? ShiftId { get; set; }

    public DateOnly? WorkDate { get; set; }

    public TimeOnly? CheckInTime { get; set; }

    public TimeOnly? CheckOutTime { get; set; }

    public AttendanceStatus? Status { get; set; }

    public string? Note { get; set; }
}