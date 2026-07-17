namespace booking_hotel_backend.Models.DTOs.WorkSchedule;

public class UpdateWorkScheduleRequest
{
    public long? ShiftId { get; set; }

    public DateOnly? WorkDate { get; set; }

    public bool? IsDayOff { get; set; }
}