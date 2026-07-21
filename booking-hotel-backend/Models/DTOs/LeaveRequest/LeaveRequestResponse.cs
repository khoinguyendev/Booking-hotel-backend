using booking_hotel_backend.Models.Enums;

namespace booking_hotel_backend.Models.DTOs.LeaveRequest;

public class LeaveRequestResponse
{
    public long Id { get; set; }

    public long StaffRequestId { get; set; }

    public long HotelStaffId { get; set; }

    public string StaffName { get; set; } = string.Empty;

    public DateOnly FromDate { get; set; }

    public DateOnly ToDate { get; set; }

    public string? Reason { get; set; }

    public RequestStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }
}