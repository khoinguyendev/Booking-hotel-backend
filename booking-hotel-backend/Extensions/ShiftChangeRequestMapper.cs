using booking_hotel_backend.Models.DTOs.ShiftChangeRequest;
using booking_hotel_backend.Models.Entities;

namespace booking_hotel_backend.Extensions;

public static class ShiftChangeRequestMapper
{
    public static ShiftChangeRequestResponse ToResponse(this ShiftChangeRequest entity)
    {
        return new ShiftChangeRequestResponse
        {
            Id = entity.Id,

            StaffRequestId = entity.StaffRequestId,

            HotelStaffId = entity.StaffRequest.HotelStaffId,

            StaffName = entity.StaffRequest.HotelStaff.User.FullName,

            EmployeeCode = entity.StaffRequest.HotelStaff.EmployeeCode,

            Status = entity.StaffRequest.Status,

            Reason = entity.StaffRequest.Reason,

            WorkScheduleId = entity.WorkScheduleId,

            CurrentWorkDate = entity.WorkSchedule.WorkDate,

            CurrentShift = entity.WorkSchedule.Shift.Name,

            TargetWorkScheduleId = entity.TargetWorkScheduleId,

            TargetEmployee = entity.TargetWorkSchedule == null
                ? null
                : entity.TargetWorkSchedule.HotelStaff.User.FullName,

            NewShiftId = entity.NewShiftId,

            NewShift = entity.NewShift?.Name,

            NewWorkDate = entity.NewWorkDate,

            CreatedAt = entity.StaffRequest.CreatedAt,

            ApprovedAt = entity.StaffRequest.ApprovedAt
        };
    }
}