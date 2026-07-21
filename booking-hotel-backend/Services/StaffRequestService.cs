using booking_hotel_backend.Common.Exceptions;
using booking_hotel_backend.Data;
using booking_hotel_backend.Models.DTOs.StaffRequest;
using booking_hotel_backend.Models.Enums;
using booking_hotel_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace booking_hotel_backend.Services;

public class StaffRequestService : IStaffRequestService
{
    private readonly AppDbContext _context;

    public StaffRequestService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<StaffRequestResponse>> GetAllAsync()
    {
        var requests = await _context.StaffRequests
            .Include(x => x.HotelStaff)
                .ThenInclude(x => x.User)
            .Include(x => x.HotelStaff)
                .ThenInclude(x => x.Position)
            .Include(x => x.LeaveRequest)
            .Include(x => x.OvertimeRequest)
            .Include(x => x.ShiftChangeRequest)
            .ThenInclude(x => x.NewShift)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();

        return requests.Select(Map).ToList();
    }
    public async Task<List<StaffRequestResponse>> GetByUserIdAsync(int userId)
    {
        // Lấy nhân viên thuộc user hiện tại
        var hotelStaff = await _context.HotelStaffs
            .FirstOrDefaultAsync(x => x.UserId == userId)
            ?? throw new BadRequestException(
                "X001",
                "Nhân viên không tồn tại."
            );


        var requests = await _context.StaffRequests
            .Include(x => x.HotelStaff)
                .ThenInclude(x => x.User)
            .Include(x => x.HotelStaff)
                .ThenInclude(x => x.Position)
            .Include(x => x.LeaveRequest)
            .Include(x => x.OvertimeRequest)
            .Include(x => x.ShiftChangeRequest)
            .Where(x => x.HotelStaffId == hotelStaff.Id)
            .Include(x => x.ShiftChangeRequest)
    .ThenInclude(x => x.WorkSchedule)
        .ThenInclude(x => x.Shift)

.Include(x => x.ShiftChangeRequest)
    .ThenInclude(x => x.NewShift)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();


        return requests
            .Select(Map)
            .ToList();
    }
    public async Task<StaffRequestResponse> GetByIdAsync(long id)
    {
        var request = await _context.StaffRequests
            .Include(x => x.HotelStaff)
                .ThenInclude(x => x.User)
            .Include(x => x.HotelStaff)
                .ThenInclude(x => x.Position)
            .Include(x => x.LeaveRequest)
            .Include(x => x.OvertimeRequest)
            .Include(x => x.ShiftChangeRequest).ThenInclude(x=>x.WorkSchedule)

            .FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception("Không tìm thấy đơn.");

        return Map(request);
    }
    public async Task DeleteAsync(long id)
    {
        var staffRequest = await _context.StaffRequests
            .FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new BadRequestException("X001", "Không tìm thấy đơn.");

        _context.StaffRequests.Remove(staffRequest);
        await _context.SaveChangesAsync();
    }
    private static StaffRequestResponse Map(Models.Entities.StaffRequest request)
    {
        object? detail = null;

        switch (request.Type)
        {
            case RequestType.Leave:

                detail = request.LeaveRequest == null
                    ? null
                    : new
                    {
                        request.LeaveRequest.FromDate,
                        request.LeaveRequest.ToDate
                    };

                break;

            case RequestType.Overtime:

                detail = request.OvertimeRequest == null
                    ? null
                    : new
                    {
                        request.OvertimeRequest.WorkDate,
                        request.OvertimeRequest.FromTime,
                        request.OvertimeRequest.ToTime,
                        request.OvertimeRequest.Hours
                    };

                break;

            case RequestType.ShiftChange:

                detail = request.ShiftChangeRequest == null
                    ? null
                    : new
                    {
                        WorkScheduleId = request.ShiftChangeRequest.WorkScheduleId,

                        CurrentDate = request.ShiftChangeRequest.WorkSchedule?.WorkDate,
                        CurrentShiftName = request.ShiftChangeRequest.WorkSchedule?.Shift?.Name,
                        CurrentShiftId = request.ShiftChangeRequest.WorkSchedule != null
                            ? request.ShiftChangeRequest.WorkSchedule.ShiftId
                            : null,

                        NewShiftId = request.ShiftChangeRequest.NewShiftId,

                        NewShiftName = request.ShiftChangeRequest.NewShift != null
                            ? request.ShiftChangeRequest.NewShift.Name
                            : null,

                        NewWorkDate = request.ShiftChangeRequest.NewWorkDate,

                        TargetWorkScheduleId = request.ShiftChangeRequest.TargetWorkScheduleId
                    };

                break;
        }

        return new StaffRequestResponse
        {
            Id = request.Id,

            HotelStaffId = request.HotelStaffId,

            EmployeeCode = request.HotelStaff.EmployeeCode,

            StaffName = request.HotelStaff.User.FullName,

            Position = request.HotelStaff.Position.Name,

            Type = request.Type,

            Status = request.Status,

            Reason = request.Reason,

            CreatedAt = request.CreatedAt,

            Detail = detail
        };
    }
}