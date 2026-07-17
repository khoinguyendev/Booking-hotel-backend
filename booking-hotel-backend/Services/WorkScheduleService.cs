using booking_hotel_backend.Data;
using booking_hotel_backend.Extensions;
using booking_hotel_backend.Models.DTOs.WorkSchedule;
using booking_hotel_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace booking_hotel_backend.Services
{
    public class WorkScheduleService:IWorkScheduleService
    {
        private readonly AppDbContext _context;
        public WorkScheduleService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<WorkScheduleResponse> CreateAsync(CreateWorkScheduleRequest request)
        {
            // Kiểm tra nhân viên tồn tại
            var hotelStaff = await _context.HotelStaffs
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == request.HotelStaffId)
                ?? throw new Exception("Nhân viên không tồn tại.");

            // Kiểm tra ca làm tồn tại
            var shift = await _context.Shifts
                .FirstOrDefaultAsync(x => x.Id == request.ShiftId)
                ?? throw new Exception("Ca làm không tồn tại.");

            // Kiểm tra trùng lịch
            var exists = await _context.WorkSchedules.AnyAsync(x =>
                x.HotelStaffId == request.HotelStaffId &&
                x.WorkDate == request.WorkDate &&
                x.ShiftId == request.ShiftId);

            if (exists)
                throw new Exception("Nhân viên đã được phân ca này.");

            var workSchedule = request.ToEntity();

            _context.WorkSchedules.Add(workSchedule);

            await _context.SaveChangesAsync();

            workSchedule = await _context.WorkSchedules
                .Include(x => x.HotelStaff)
                    .ThenInclude(x => x.User)
                .Include(x => x.Shift)
                .FirstAsync(x => x.Id == workSchedule.Id);

            return workSchedule.ToResponse();
        }

        public async Task<List<WorkScheduleResponse>> GetAllAsync()
        {
            var schedules = await _context.WorkSchedules
                .Include(x => x.HotelStaff)
                    .ThenInclude(x => x.User)
                .Include(x => x.Shift)
                .OrderByDescending(x => x.WorkDate)
                .ThenBy(x => x.Shift.StartTime)
                .ToListAsync();

            return schedules.Select(x => x.ToResponse()).ToList();
        }

        public async Task<WorkScheduleResponse> GetByIdAsync(long id)
        {
            var schedule = await _context.WorkSchedules
                .Include(x => x.HotelStaff)
                    .ThenInclude(x => x.User)
                .Include(x => x.Shift)
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new Exception("Lịch làm không tồn tại.");

            return schedule.ToResponse();
        }
        public async Task<WorkScheduleResponse> UpdateAsync(long id, UpdateWorkScheduleRequest request)
        {
            var schedule = await _context.WorkSchedules
                .Include(x => x.HotelStaff)
                .Include(x => x.Shift)
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new Exception("Lịch làm không tồn tại.");

            // Giá trị sau khi cập nhật
            var shiftId = request.ShiftId ?? schedule.ShiftId;
            var workDate = request.WorkDate ?? schedule.WorkDate;
            var isDayOff = request.IsDayOff ?? schedule.IsDayOff;

            // Nếu không phải ngày nghỉ thì kiểm tra ca làm
            if (!isDayOff)
            {
                var shiftExists = await _context.Shifts
                    .AnyAsync(x => x.Id == shiftId);

                if (!shiftExists)
                    throw new Exception("Ca làm không tồn tại.");
            }

            // Kiểm tra trùng lịch (bỏ qua chính bản ghi đang sửa)
            var exists = await _context.WorkSchedules.AnyAsync(x =>
                x.Id != id &&
                x.HotelStaffId == schedule.HotelStaffId &&
                x.WorkDate == workDate &&
                x.ShiftId == shiftId);

            if (exists)
                throw new Exception("Nhân viên đã được phân ca.");
            var hasAttendance = await _context.Attendances
    .AnyAsync(x => x.WorkScheduleId == schedule.Id);

            if (hasAttendance)
                throw new Exception("Không thể xóa lịch làm vì đã có dữ liệu chấm công.");
            // Cập nhật dữ liệu
            schedule.UpdateFromRequest(request);

            await _context.SaveChangesAsync();

            // Lấy lại đầy đủ navigation để trả về
            schedule = await _context.WorkSchedules
                .Include(x => x.HotelStaff)
                    .ThenInclude(x => x.User)
                .Include(x => x.Shift)
                .FirstAsync(x => x.Id == id);

            return schedule.ToResponse();
        }

        public async Task DeleteAsync(long id)
        {
            var schedule = await _context.WorkSchedules
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new Exception("Lịch làm không tồn tại.");

            // Không cho xóa nếu đã có chấm công
            var hasAttendance = await _context.Attendances
     .AnyAsync(x => x.WorkScheduleId == schedule.Id);

            if (hasAttendance)
                throw new Exception("Không thể sửa lịch làm vì đã phát sinh chấm công.");

            _context.WorkSchedules.Remove(schedule);

            await _context.SaveChangesAsync();
        }
        private async Task ValidateAsync(
    long hotelStaffId,
    long shiftId,
    DateOnly workDate,
    long? ignoreId = null)
        {
            var staffExists = await _context.HotelStaffs
                .AnyAsync(x => x.Id == hotelStaffId);

            if (!staffExists)
                throw new Exception("Nhân viên không tồn tại.");

            var shiftExists = await _context.Shifts
                .AnyAsync(x => x.Id == shiftId);

            if (!shiftExists)
                throw new Exception("Ca làm không tồn tại.");

            var query = _context.WorkSchedules.Where(x =>
                x.HotelStaffId == hotelStaffId &&
                x.WorkDate == workDate &&
                x.ShiftId == shiftId);

            if (ignoreId.HasValue)
                query = query.Where(x => x.Id != ignoreId.Value);

            var exists = await query.AnyAsync();

            if (exists)
                throw new Exception("Nhân viên đã được phân ca.");
        }
    }
}
