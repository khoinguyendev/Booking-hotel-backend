using booking_hotel_backend.Models.DTOs.Attendance;
using booking_hotel_backend.Models.Entities;

namespace booking_hotel_backend.Extensions;

public static class AttendanceMapper
{
    public static Attendance ToEntity(this CreateAttendanceRequest request)
    {
        return new Attendance
        {
            CheckInTime = request.CheckInTime,
            CheckOutTime = request.CheckOutTime,
            Status = request.Status,
            Note = request.Note
        };
    }

    public static void UpdateFromRequest(this Attendance attendance, UpdateAttendanceRequest request)
    {
        attendance.CheckInTime = request.CheckInTime ?? attendance.CheckInTime;
        attendance.CheckOutTime = request.CheckOutTime ?? attendance.CheckOutTime;
        attendance.Status = request.Status ?? attendance.Status;
        attendance.Note = request.Note ?? attendance.Note;
    }

    public static AttendanceResponse ToResponse(this Attendance attendance)
    {
        return new AttendanceResponse
        {
            Id = attendance.Id,

            ShiftTime =
    $"{attendance.WorkSchedule.Shift.StartTime:HH\\:mm} - {attendance.WorkSchedule.Shift.EndTime:HH\\:mm}",

            CheckInTime = attendance.CheckInTime,
            CheckOutTime = attendance.CheckOutTime,
            Status = attendance.Status,
            Note = attendance.Note
        };
    }
}