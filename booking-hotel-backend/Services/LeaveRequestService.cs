using booking_hotel_backend.Common.Exceptions;
using booking_hotel_backend.Data;
using booking_hotel_backend.Extensions;
using booking_hotel_backend.Models.DTOs.LeaveRequest;
using booking_hotel_backend.Models.Entities;
using booking_hotel_backend.Models.Enums;
using booking_hotel_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace booking_hotel_backend.Services;

public class LeaveRequestService : ILeaveRequestService
{
    private readonly AppDbContext _context;

    public LeaveRequestService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<LeaveRequestResponse>> GetAllAsync()
    {
        var requests = await _context.LeaveRequests
            .Include(x => x.StaffRequest)
                .ThenInclude(x => x.HotelStaff)
                    .ThenInclude(x => x.User)
            .OrderByDescending(x => x.StaffRequest.CreatedAt)
            .ToListAsync();

        return requests
            .Select(x => x.ToResponse())
            .ToList();
    }
    public async Task<LeaveRequestResponse> GetByIdAsync(long id)
    {
        var leave = await _context.LeaveRequests
            .Include(x => x.StaffRequest)
                .ThenInclude(x => x.HotelStaff)
                    .ThenInclude(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception("Không tìm thấy đơn nghỉ phép.");

        return leave.ToResponse();
    }

    public async Task<List<LeaveRequestResponse>> GetByStaffAsync(long hotelStaffId)
    {
        var requests = await _context.LeaveRequests
            .Include(x => x.StaffRequest)
                .ThenInclude(x => x.HotelStaff)
                    .ThenInclude(x => x.User)
            .Where(x => x.StaffRequest.HotelStaffId == hotelStaffId)
            .OrderByDescending(x => x.StaffRequest.CreatedAt)
            .ToListAsync();

        return requests.Select(x => x.ToResponse()).ToList();
    }
    public async Task<LeaveRequestResponse> CreateAsync(int userId,CreateLeaveRequestRequest request)
    {
        // Kiểm tra nhân viên
        var hotelStaff = await _context.HotelStaffs
    .Include(x => x.User)
    .Include(x => x.Hotel)
    .FirstOrDefaultAsync(x => x.UserId == userId)
    ?? throw new BadRequestException("X001", "Nhân viên không tồn tại.");

        // Kiểm tra ngày
        if (request.FromDate > request.ToDate)
            throw new BadRequestException("X002", "Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc.");

        // Không cho tạo đơn nghỉ trong quá khứ
        if (request.FromDate < DateOnly.FromDateTime(DateTime.Today))
            throw new BadRequestException("X004", "Không thể tạo đơn nghỉ cho ngày đã qua.");

        // Kiểm tra đơn Pending bị trùng thời gian
        var exists = await _context.LeaveRequests
            .Include(x => x.StaffRequest)
            .AnyAsync(x =>
                x.StaffRequest.HotelStaffId == hotelStaff.Id &&
                x.StaffRequest.Status == RequestStatus.Pending &&
                request.FromDate <= x.ToDate &&
                request.ToDate >= x.FromDate);

        if (exists)
            throw new BadRequestException("X005", "Đã tồn tại đơn nghỉ đang chờ duyệt trong khoảng thời gian này.");

        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var staffRequest = new StaffRequest
            {
                HotelStaffId = hotelStaff.Id,
                Type = RequestType.Leave,
                Status = RequestStatus.Pending,
                Reason = request.Reason,
                CreatedAt = DateTime.UtcNow
            };

            _context.StaffRequests.Add(staffRequest);

            await _context.SaveChangesAsync();

            var leaveRequest = new LeaveRequest
            {
                StaffRequestId = staffRequest.Id,
                FromDate = request.FromDate,
                ToDate = request.ToDate
            };

            _context.LeaveRequests.Add(leaveRequest);

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            leaveRequest = await _context.LeaveRequests
                .Include(x => x.StaffRequest)
                    .ThenInclude(x => x.HotelStaff)
                        .ThenInclude(x => x.User)
                .FirstAsync(x => x.Id == leaveRequest.Id);

            return leaveRequest.ToResponse();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
    public async Task ApproveAsync(long id, long approvedBy)
    {
        var leaveRequest = await _context.LeaveRequests
            .Include(x => x.StaffRequest)
            .FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception("Không tìm thấy đơn nghỉ.");

        if (leaveRequest.StaffRequest.Status != RequestStatus.Pending)
            throw new Exception("Đơn đã được xử lý.");

        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            leaveRequest.StaffRequest.Status = RequestStatus.Approved;
            leaveRequest.StaffRequest.ApprovedBy = approvedBy;
            leaveRequest.StaffRequest.ApprovedAt = DateTime.UtcNow;

            // Cập nhật lịch làm thành ngày nghỉ
            var schedules = await _context.WorkSchedules
                .Where(x =>
                    x.HotelStaffId == leaveRequest.StaffRequest.HotelStaffId &&
                    x.WorkDate >= leaveRequest.FromDate &&
                    x.WorkDate <= leaveRequest.ToDate)
                .ToListAsync();

            foreach (var schedule in schedules)
            {
                schedule.IsDayOff = true;
                var hasAttendance = await _context.Attendances.AnyAsync(x =>
    schedules.Select(s => s.Id).Contains(x.WorkScheduleId));

                if (hasAttendance)
                    throw new Exception("Không thể duyệt vì đã phát sinh chấm công.");
            }

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
    public async Task<LeaveRequestResponse> UpdateAsync(long id, UpdateLeaveRequestRequest request)
    {
        var leaveRequest = await _context.LeaveRequests
            .Include(x => x.StaffRequest)
                .ThenInclude(x => x.HotelStaff)
                    .ThenInclude(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception("Không tìm thấy đơn nghỉ.");

        if (leaveRequest.StaffRequest.Status != RequestStatus.Pending)
            throw new Exception("Đơn đã được xử lý, không thể chỉnh sửa.");

        var fromDate = request.FromDate ?? leaveRequest.FromDate;
        var toDate = request.ToDate ?? leaveRequest.ToDate;

        if (fromDate > toDate)
            throw new Exception("Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc.");

        var exists = await _context.LeaveRequests
            .Include(x => x.StaffRequest)
            .AnyAsync(x =>
                x.Id != id &&
                x.StaffRequest.HotelStaffId == leaveRequest.StaffRequest.HotelStaffId &&
                x.StaffRequest.Status == RequestStatus.Pending &&
                fromDate <= x.ToDate &&
                toDate >= x.FromDate);

        if (exists)
            throw new Exception("Đã có đơn nghỉ khác bị trùng thời gian.");

        if (request.FromDate.HasValue)
            leaveRequest.FromDate = request.FromDate.Value;

        if (request.ToDate.HasValue)
            leaveRequest.ToDate = request.ToDate.Value;

        if (!string.IsNullOrWhiteSpace(request.Reason))
            leaveRequest.StaffRequest.Reason = request.Reason;

        await _context.SaveChangesAsync();

        return leaveRequest.ToResponse();
    }
    public async Task RejectAsync(long id, string rejectReason, long approvedBy)
    {
        var leaveRequest = await _context.LeaveRequests
            .Include(x => x.StaffRequest)
            .FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception("Không tìm thấy đơn nghỉ.");

        if (leaveRequest.StaffRequest.Status != RequestStatus.Pending)
            throw new Exception("Đơn đã được xử lý.");

        leaveRequest.StaffRequest.Status = RequestStatus.Rejected;
        leaveRequest.StaffRequest.RejectReason = rejectReason;
        leaveRequest.StaffRequest.ApprovedBy = approvedBy;
        leaveRequest.StaffRequest.ApprovedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(long id)
    {
        var leaveRequest = await _context.LeaveRequests
            .Include(x => x.StaffRequest)
            .FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception("Không tìm thấy đơn nghỉ.");

        if (leaveRequest.StaffRequest.Status != RequestStatus.Pending)
            throw new Exception("Chỉ được xóa đơn đang chờ duyệt.");

        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            _context.LeaveRequests.Remove(leaveRequest);

            _context.StaffRequests.Remove(leaveRequest.StaffRequest);

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}