using System.ComponentModel.DataAnnotations;

namespace booking_hotel_backend.Models.DTOs.LeaveRequest;

public class UpdateLeaveRequestRequest
{
    public DateOnly? FromDate { get; set; }

    public DateOnly? ToDate { get; set; }

    [MaxLength(500)]
    public string? Reason { get; set; }
}