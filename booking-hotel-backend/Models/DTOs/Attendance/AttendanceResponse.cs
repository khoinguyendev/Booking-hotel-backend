using booking_hotel_backend.Models.Entities;

namespace booking_hotel_backend.Models.DTOs.Attendance;

public class AttendanceResponse
{
    public long Id { get; set; }
    public string ShiftTime { get; set; } = null!;
    public TimeOnly? CheckInTime { get; set; }

    public TimeOnly? CheckOutTime { get; set; }

    public AttendanceStatus Status { get; set; }

    public string? Note { get; set; }
}