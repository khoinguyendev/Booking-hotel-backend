using booking_hotel_backend.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace booking_hotel_backend.Models.DTOs.Attendance;

public class CreateAttendanceRequest
{
    [Required]
    public long HotelStaffId { get; set; }

    [Required]
    public long ShiftId { get; set; }

    [Required]
    public DateOnly WorkDate { get; set; }

    public TimeOnly? CheckInTime { get; set; }

    public TimeOnly? CheckOutTime { get; set; }

    public AttendanceStatus Status { get; set; }

    public string? Note { get; set; }
}