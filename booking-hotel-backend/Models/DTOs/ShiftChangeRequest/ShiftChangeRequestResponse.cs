using booking_hotel_backend.Models.Enums;

namespace booking_hotel_backend.Models.DTOs.ShiftChangeRequest;

public class ShiftChangeRequestResponse
{
    public long Id { get; set; }

    public long StaffRequestId { get; set; }

    public long HotelStaffId { get; set; }

    public string StaffName { get; set; } = string.Empty;

    public string EmployeeCode { get; set; } = string.Empty;

    public RequestStatus Status { get; set; }

    public string? Reason { get; set; }

    // Lịch hiện tại
    public long WorkScheduleId { get; set; }

    public DateOnly CurrentWorkDate { get; set; }

    public string CurrentShift { get; set; } = string.Empty;

    // Đổi với người khác
    public long? TargetWorkScheduleId { get; set; }

    public string? TargetEmployee { get; set; }

    // Ca muốn đổi
    public long? NewShiftId { get; set; }

    public string? NewShift { get; set; }

    public DateOnly? NewWorkDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? ApprovedAt { get; set; }
}