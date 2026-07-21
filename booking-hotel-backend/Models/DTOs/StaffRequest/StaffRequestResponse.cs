using booking_hotel_backend.Models.Enums;

namespace booking_hotel_backend.Models.DTOs.StaffRequest;

public class StaffRequestResponse
{
    public long Id { get; set; }

    public long HotelStaffId { get; set; }

    public string EmployeeCode { get; set; } = string.Empty;

    public string StaffName { get; set; } = string.Empty;

    public string Position { get; set; } = string.Empty;

    public RequestType Type { get; set; }

    public RequestStatus Status { get; set; }

    public string? Reason { get; set; }

    public DateTime CreatedAt { get; set; }

    // Chi tiết từng loại đơn
    public object? Detail { get; set; }
}