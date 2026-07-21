using System.ComponentModel.DataAnnotations;

namespace booking_hotel_backend.Models.DTOs.ShiftChangeRequest;

public class UpdateShiftChangeRequest
{
    public long? TargetWorkScheduleId { get; set; }

    public long? NewShiftId { get; set; }

    public DateOnly? NewWorkDate { get; set; }

    [MaxLength(500)]
    public string? Reason { get; set; }
}