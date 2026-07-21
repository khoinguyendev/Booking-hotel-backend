using booking_hotel_backend.Models.DTOs.LeaveRequest;
using booking_hotel_backend.Models.Entities;

namespace booking_hotel_backend.Extensions;

public static class LeaveRequestMapper
{
    public static LeaveRequestResponse ToResponse(this LeaveRequest leave)
    {
        return new LeaveRequestResponse
        {
            Id = leave.Id,
            StaffRequestId = leave.StaffRequestId,

            HotelStaffId = leave.StaffRequest.HotelStaffId,
            StaffName = leave.StaffRequest.HotelStaff.User.FullName,

            FromDate = leave.FromDate,
            ToDate = leave.ToDate,

            Reason = leave.StaffRequest.Reason,

            Status = leave.StaffRequest.Status,

            CreatedAt = leave.StaffRequest.CreatedAt
        };
    }
}