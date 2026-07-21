using System.ComponentModel.DataAnnotations;

namespace booking_hotel_backend.Models.DTOs.ShiftChangeRequest;

public class CreateShiftChangeRequest
{
    

    /// <summary>
    /// Lịch hiện tại muốn đổi
    /// </summary>
    [Required]
    public long WorkScheduleId { get; set; }

    /// <summary>
    /// Nếu đổi với nhân viên khác
    /// </summary>
    public long? TargetWorkScheduleId { get; set; }

    /// <summary>
    /// Nếu không đổi với ai thì chọn ca mới
    /// </summary>
    public long? NewShiftId { get; set; }

    /// <summary>
    /// Có thể để null nếu giữ nguyên ngày
    /// </summary>
    public DateOnly? NewWorkDate { get; set; }

    [MaxLength(500)]
    public string? Reason { get; set; }
}