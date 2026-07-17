using booking_hotel_backend.Models.DTOs.WorkSchedule;
using booking_hotel_backend.Models.Entities;

namespace booking_hotel_backend.Extensions;

public static class WorkScheduleMapper
{
    public static WorkSchedule ToEntity(this CreateWorkScheduleRequest request)
    {
        return new WorkSchedule
        {
            HotelStaffId = request.HotelStaffId,
            ShiftId = request.ShiftId,
            WorkDate = request.WorkDate,
            IsDayOff = request.IsDayOff
        };
    }

    public static void UpdateFromRequest(this WorkSchedule schedule, UpdateWorkScheduleRequest request)
    {
        schedule.ShiftId = request.ShiftId ?? schedule.ShiftId;
        schedule.WorkDate = request.WorkDate ?? schedule.WorkDate;
        schedule.IsDayOff = request.IsDayOff ?? schedule.IsDayOff;
    }

    public static WorkScheduleResponse ToResponse(this WorkSchedule schedule)
    {
        return new WorkScheduleResponse
        {
            Id = schedule.Id,
            //HotelStaffId = schedule.HotelStaffId,
            //StaffName = schedule.HotelStaff.User.FullName, // hoặc trường phù hợp
            //ShiftId = schedule.ShiftId,
            //ShiftName = schedule.Shift.Name,
            WorkDate = schedule.WorkDate,
            IsDayOff = schedule.IsDayOff
        };
    }
}