using booking_hotel_backend.Common.Exceptions;
using booking_hotel_backend.Data;
using booking_hotel_backend.Extensions;
using booking_hotel_backend.Models.DTOs.ShiftChangeRequest;
using booking_hotel_backend.Models.Entities;
using booking_hotel_backend.Models.Enums;
using booking_hotel_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace booking_hotel_backend.Services;

public class ShiftChangeRequestService : IShiftChangeRequestService
{
    private readonly AppDbContext _context;

    public ShiftChangeRequestService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ShiftChangeRequestResponse> CreateAsync(int userId,CreateShiftChangeRequest request)
    {

        // Kiểm tra nhân viên

        var staff = await _context.HotelStaffs
            .Include(x => x.User)
            .Include(x=>x.Hotel)
            .FirstOrDefaultAsync(x => x.UserId == userId)
            ?? throw new BadRequestException("X001", "Nhân viên không tồn tại.");

        var schedule = await _context.WorkSchedules
            .Include(x => x.Shift)
            .FirstOrDefaultAsync(x => x.Id == request.WorkScheduleId)
            ?? throw new Exception("Lịch làm không tồn tại.");

        if (schedule.HotelStaffId != staff.Id)
            throw new Exception("Lịch làm không thuộc nhân viên.");

        WorkSchedule? targetSchedule = null;

        if (request.TargetWorkScheduleId.HasValue)
        {
            targetSchedule = await _context.WorkSchedules
                .Include(x => x.Shift)
                .Include(x => x.HotelStaff)
                    .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == request.TargetWorkScheduleId.Value)
                ?? throw new Exception("Không tìm thấy lịch muốn đổi.");

            if (targetSchedule.HotelStaffId == staff.Id)
                throw new Exception("Không thể đổi với chính mình.");
        }

        if (!request.TargetWorkScheduleId.HasValue)
        {
            if (!request.NewShiftId.HasValue)
                throw new Exception("Vui lòng chọn ca mới.");

            var shiftExists = await _context.Shifts
                .AnyAsync(x => x.Id == request.NewShiftId.Value);

            if (!shiftExists)
                throw new Exception("Ca làm không tồn tại.");
        }

        var staffRequest = new StaffRequest
        {
            HotelStaffId = staff.Id,
            Type = RequestType.ShiftChange,
            Status = RequestStatus.Pending,
            Reason = request.Reason,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.StaffRequests.Add(staffRequest);

        await _context.SaveChangesAsync();

        var shiftRequest = new ShiftChangeRequest
        {
            StaffRequestId = staffRequest.Id,
            WorkScheduleId = request.WorkScheduleId,
            TargetWorkScheduleId = request.TargetWorkScheduleId,
            NewShiftId = request.NewShiftId,
            NewWorkDate = request.NewWorkDate
        };

        _context.ShiftChangeRequests.Add(shiftRequest);

        await _context.SaveChangesAsync();

        var result = await _context.ShiftChangeRequests
            .Include(x => x.StaffRequest)
                .ThenInclude(x => x.HotelStaff)
                    .ThenInclude(x => x.User)
            .Include(x => x.WorkSchedule)
                .ThenInclude(x => x.Shift)
            .Include(x => x.TargetWorkSchedule)
                .ThenInclude(x => x.HotelStaff)
                    .ThenInclude(x => x.User)
            .Include(x => x.NewShift)
            .FirstAsync(x => x.Id == shiftRequest.Id);

        return result.ToResponse();
    }

    public async Task<ShiftChangeRequestResponse> UpdateAsync(long id, UpdateShiftChangeRequest request)
    {
        var entity = await _context.ShiftChangeRequests
            .Include(x => x.StaffRequest)
                .ThenInclude(x => x.HotelStaff)
                    .ThenInclude(x => x.User)
            .Include(x => x.WorkSchedule)
                .ThenInclude(x => x.Shift)
            .Include(x => x.TargetWorkSchedule)
                .ThenInclude(x => x.HotelStaff)
                    .ThenInclude(x => x.User)
            .Include(x => x.NewShift)
            .FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception("Yêu cầu đổi ca không tồn tại.");

        if (entity.StaffRequest.Status != RequestStatus.Pending)
            throw new Exception("Đơn đã được xử lý, không thể chỉnh sửa.");

        if (request.TargetWorkScheduleId.HasValue)
        {
            var target = await _context.WorkSchedules
                .Include(x => x.HotelStaff)
                .FirstOrDefaultAsync(x => x.Id == request.TargetWorkScheduleId.Value)
                ?? throw new Exception("Lịch muốn đổi không tồn tại.");

            if (target.HotelStaffId == entity.StaffRequest.HotelStaffId)
                throw new Exception("Không thể đổi ca với chính mình.");

            entity.TargetWorkScheduleId = request.TargetWorkScheduleId;
            entity.NewShiftId = null;
        }
        else
        {
            if (!request.NewShiftId.HasValue)
                throw new Exception("Vui lòng chọn ca mới.");

            var shiftExists = await _context.Shifts
                .AnyAsync(x => x.Id == request.NewShiftId.Value);

            if (!shiftExists)
                throw new Exception("Ca làm không tồn tại.");

            entity.TargetWorkScheduleId = null;
            entity.NewShiftId = request.NewShiftId;
        }

        entity.NewWorkDate = request.NewWorkDate;

        entity.StaffRequest.Reason = request.Reason;
        entity.StaffRequest.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        entity = await _context.ShiftChangeRequests
            .Include(x => x.StaffRequest)
                .ThenInclude(x => x.HotelStaff)
                    .ThenInclude(x => x.User)
            .Include(x => x.WorkSchedule)
                .ThenInclude(x => x.Shift)
            .Include(x => x.TargetWorkSchedule)
                .ThenInclude(x => x.HotelStaff)
                    .ThenInclude(x => x.User)
            .Include(x => x.NewShift)
            .FirstAsync(x => x.Id == id);

        return entity.ToResponse();
    }

    public async Task<ShiftChangeRequestResponse> ApproveAsync(long id, long managerId)
    {
        var entity = await _context.ShiftChangeRequests
            .Include(x => x.StaffRequest)
                .ThenInclude(x => x.HotelStaff)
                    .ThenInclude(x => x.User)
            .Include(x => x.WorkSchedule)
            .Include(x => x.TargetWorkSchedule)
            .Include(x => x.NewShift)
            .FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception("Yêu cầu đổi ca không tồn tại.");

        if (entity.StaffRequest.Status != RequestStatus.Pending)
            throw new Exception("Đơn đã được xử lý.");

        // ==========================
        // ĐỔI CA VỚI NHÂN VIÊN KHÁC
        // ==========================
        if (entity.TargetWorkScheduleId.HasValue)
        {
            var targetSchedule = await _context.WorkSchedules
                .FirstOrDefaultAsync(x => x.Id == entity.TargetWorkScheduleId.Value)
                ?? throw new Exception("Không tìm thấy lịch của nhân viên cần đổi.");

            // Hoán đổi ca
            var shiftId = entity.WorkSchedule.ShiftId;
            entity.WorkSchedule.ShiftId = targetSchedule.ShiftId;
            targetSchedule.ShiftId = shiftId;

            // Hoán đổi ngày
            var workDate = entity.WorkSchedule.WorkDate;
            entity.WorkSchedule.WorkDate = targetSchedule.WorkDate;
            targetSchedule.WorkDate = workDate;
        }
        else
        {
            // ==========================
            // ĐỔI CA / ĐỔI NGÀY
            // ==========================

            if (entity.NewShiftId.HasValue)
            {
                entity.WorkSchedule.ShiftId = entity.NewShiftId.Value;
            }

            if (entity.NewWorkDate.HasValue)
            {
                entity.WorkSchedule.WorkDate = entity.NewWorkDate.Value;
            }
        }

        entity.StaffRequest.Status = RequestStatus.Approved;
        entity.StaffRequest.ApprovedBy = managerId;
        entity.StaffRequest.ApprovedAt = DateTime.UtcNow;
        entity.StaffRequest.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        entity = await _context.ShiftChangeRequests
            .Include(x => x.StaffRequest)
                .ThenInclude(x => x.HotelStaff)
                    .ThenInclude(x => x.User)
            .Include(x => x.WorkSchedule)
                .ThenInclude(x => x.Shift)
            .Include(x => x.TargetWorkSchedule)
                .ThenInclude(x => x.HotelStaff)
                    .ThenInclude(x => x.User)
            .Include(x => x.NewShift)
            .FirstAsync(x => x.Id == id);

        return entity.ToResponse();
    }

    public async Task<ShiftChangeRequestResponse> RejectAsync(
    long id,
    string reason,
    long managerId)
    {
        var entity = await _context.ShiftChangeRequests
            .Include(x => x.StaffRequest)
                .ThenInclude(x => x.HotelStaff)
                    .ThenInclude(x => x.User)
            .Include(x => x.WorkSchedule)
                .ThenInclude(x => x.Shift)
            .Include(x => x.TargetWorkSchedule)
                .ThenInclude(x => x.HotelStaff)
                    .ThenInclude(x => x.User)
            .Include(x => x.NewShift)
            .FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception("Yêu cầu đổi ca không tồn tại.");

        if (entity.StaffRequest.Status != RequestStatus.Pending)
            throw new Exception("Đơn đã được xử lý.");

        entity.StaffRequest.Status = RequestStatus.Rejected;
        entity.StaffRequest.RejectReason = reason;
        entity.StaffRequest.ApprovedBy = managerId;
        entity.StaffRequest.ApprovedAt = DateTime.UtcNow;
        entity.StaffRequest.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return entity.ToResponse();
    }
    public async Task DeleteAsync(long id)
    {
        var entity = await _context.ShiftChangeRequests
            .Include(x => x.StaffRequest)
            .FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception("Yêu cầu đổi ca không tồn tại.");

        if (entity.StaffRequest.Status != RequestStatus.Pending)
            throw new Exception("Không thể xóa đơn đã được xử lý.");

        _context.ShiftChangeRequests.Remove(entity);
        _context.StaffRequests.Remove(entity.StaffRequest);

        await _context.SaveChangesAsync();
    }
    public async Task<ShiftChangeRequestResponse> GetByIdAsync(long id)
    {
        var entity = await _context.ShiftChangeRequests
            .Include(x => x.StaffRequest)
                .ThenInclude(x => x.HotelStaff)
                    .ThenInclude(x => x.User)
            .Include(x => x.WorkSchedule)
                .ThenInclude(x => x.Shift)
            .Include(x => x.TargetWorkSchedule)
                .ThenInclude(x => x.HotelStaff)
                    .ThenInclude(x => x.User)
            .Include(x => x.NewShift)
            .FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception("Không tìm thấy yêu cầu.");

        return entity.ToResponse();
    }
    public async Task<List<ShiftChangeRequestResponse>> GetAllAsync()
    {
        var data = await _context.ShiftChangeRequests
            .Include(x => x.StaffRequest)
                .ThenInclude(x => x.HotelStaff)
                    .ThenInclude(x => x.User)
            .Include(x => x.WorkSchedule)
                .ThenInclude(x => x.Shift)
            .Include(x => x.TargetWorkSchedule)
                .ThenInclude(x => x.HotelStaff)
                    .ThenInclude(x => x.User)
            .Include(x => x.NewShift)
            .OrderByDescending(x => x.StaffRequest.CreatedAt)
            .ToListAsync();

        return data.Select(x => x.ToResponse()).ToList();
    }
}
    