namespace booking_hotel_backend.Models.DTOs.Attendance;

public class CheckInRequest
{
    public long WorkScheduleId { get; set; }

    public string? Note { get; set; }
}