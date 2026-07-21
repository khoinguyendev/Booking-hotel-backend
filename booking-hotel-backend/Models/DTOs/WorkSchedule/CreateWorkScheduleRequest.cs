using System.ComponentModel.DataAnnotations;

namespace booking_hotel_backend.Models.DTOs.WorkSchedule;

public class CreateWorkScheduleRequest
{
    public long HotelStaffId { get; set; }

    [Required]
    public long ShiftId { get; set; }

    [Required]
    public DateOnly WorkDate { get; set; }

    public bool IsDayOff { get; set; }
}