using booking_hotel_backend.Models.DTOs.Attendance;
using booking_hotel_backend.Models.DTOs.Shift;

public class WorkScheduleResponse
{
    public long Id { get; set; }

    public DateOnly WorkDate { get; set; }

    public bool IsDayOff { get; set; }

    public ShiftReponse? Shift { get; set; }

    public AttendanceResponse? Attendance { get; set; }
}