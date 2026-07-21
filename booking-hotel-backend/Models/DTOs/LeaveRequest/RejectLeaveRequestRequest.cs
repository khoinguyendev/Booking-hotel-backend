using System.ComponentModel.DataAnnotations;

namespace booking_hotel_backend.Models.DTOs.LeaveRequest;

public class RejectLeaveRequestRequest
{
    [Required]
    [MaxLength(500)]
    public string Reason { get; set; } = string.Empty;
}