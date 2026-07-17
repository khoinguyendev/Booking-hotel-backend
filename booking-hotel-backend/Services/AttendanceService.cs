using booking_hotel_backend.Data;
using booking_hotel_backend.Extensions;
using booking_hotel_backend.Models.DTOs.Attendance;
using booking_hotel_backend.Models.Entities;
using booking_hotel_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace booking_hotel_backend.Services
{
    public class AttendanceService : IAttendanceService

    {
        private readonly AppDbContext _context;

        public AttendanceService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AttendanceResponse> CheckInAsync(CheckInRequest request)
        {
            var schedule = await _context.WorkSchedules
                .Include(x => x.Shift)
                .Include(x => x.HotelStaff)
                    .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == request.WorkScheduleId)
                ?? throw new Exception("Không tìm thấy lịch làm.");

            if (schedule.IsDayOff)
                throw new Exception("Hôm nay là ngày nghỉ.");

            var attendance = await _context.Attendances
                .Include(x => x.WorkSchedule)
                    .ThenInclude(x => x.Shift)
                .Include(x => x.WorkSchedule)
                    .ThenInclude(x => x.HotelStaff)
                        .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.WorkScheduleId == schedule.Id);

            if (attendance != null && attendance.CheckInTime != null)
                throw new Exception("Bạn đã check-in.");

            if (attendance == null)
            {
                attendance = new Attendance
                {
                    WorkScheduleId = schedule.Id,
                    CheckInTime = TimeOnly.FromDateTime(DateTime.Now),
                    Status = TimeOnly.FromDateTime(DateTime.Now) <= schedule.Shift.StartTime
                        ? AttendanceStatus.Present
                        : AttendanceStatus.Late,
                    Note = request.Note
                };

                _context.Attendances.Add(attendance);
            }
            else
            {
                attendance.CheckInTime = TimeOnly.FromDateTime(DateTime.Now);
                attendance.Status = attendance.CheckInTime <= schedule.Shift.StartTime
                    ? AttendanceStatus.Present
                    : AttendanceStatus.Late;
                attendance.Note = request.Note;
            }

            await _context.SaveChangesAsync();

            attendance = await _context.Attendances
                .Include(x => x.WorkSchedule)
                    .ThenInclude(x => x.Shift)
                .Include(x => x.WorkSchedule)
                    .ThenInclude(x => x.HotelStaff)
                        .ThenInclude(x => x.User)
                .FirstAsync(x => x.Id == attendance.Id);

            return attendance.ToResponse();
        }

        public async Task<AttendanceResponse> CheckOutAsync(CheckOutRequest request)
        {
            var attendance = await _context.Attendances
                .Include(x => x.WorkSchedule)
                    .ThenInclude(x => x.Shift)
                .Include(x => x.WorkSchedule)
                    .ThenInclude(x => x.HotelStaff)
                        .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.WorkScheduleId == request.WorkScheduleId)
                ?? throw new Exception("Bạn chưa check-in.");

            if (attendance.CheckInTime == null)
                throw new Exception("Bạn chưa check-in.");

            if (attendance.CheckOutTime != null)
                throw new Exception("Bạn đã check-out.");

            attendance.CheckOutTime = TimeOnly.FromDateTime(DateTime.Now);

            if (!string.IsNullOrWhiteSpace(request.Note))
                attendance.Note = request.Note;

            await _context.SaveChangesAsync();

            return attendance.ToResponse();
        }
    }
}
