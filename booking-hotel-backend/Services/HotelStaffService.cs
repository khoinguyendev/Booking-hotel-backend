using booking_hotel_backend.Common;
using booking_hotel_backend.Common.Exceptions;
using booking_hotel_backend.Data;
using booking_hotel_backend.Extensions;
using booking_hotel_backend.Models.DTOs.Attendance;
using booking_hotel_backend.Models.DTOs.HotelStaff;
using booking_hotel_backend.Models.DTOs.Shift;
using booking_hotel_backend.Models.Entities;
using booking_hotel_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace booking_hotel_backend.Services
{
    public class HotelStaffService : IHotellStaffService
    {
        private readonly AppDbContext _context;
        public HotelStaffService(
           AppDbContext context
      )
        {
            _context = context;

        }

        public async Task CreateStaff(CreateHotelStaffRequest request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var user = request.ToEntity();

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                var hotelStaff = request.ToEntity(user.Id);

                _context.HotelStaffs.Add(hotelStaff);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task<object> Test()
        {
            var test = await _context.HotelStaffs
                        .Where(x => x.Id == 4)
                        .Select(x => new
                        {
                            x.Id,
                            Count = x.WorkSchedules.Count()
                        })
                        .FirstAsync();
            return test;
        }
        public async Task<PagedResponse<HotelStaffAttendanceResponse>> GetAttendanceStaffByManagerAsync(PaginationRequest request,int userId, DateOnly? workDate = null)
        {
            var manager = await _context.HotelStaffs
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (manager == null)
                throw new Exception("Bạn không thuộc khách sạn nào.");
            var date = workDate ?? DateOnly.FromDateTime(DateTime.Today);

            var query = _context.HotelStaffs
                .AsNoTracking()
                .Where(x => x.HotelId == manager.HotelId)
                .OrderByDescending(x => x.JoinedAt)
                .Select(HotelStaffMapper.ToAttendanceResponse(date));

            return await query.ToPagedResponseAsync(request.Page, request.PageSize);
        }

        public async Task<List<HotelStaffResponse>> GetStaffOfManagerAsync(long hotelId)
        {
            if (hotelId <= 0)
                throw new BadRequestException("X001", "Bạn không thuộc khách sạn nào.");
            Console.WriteLine(hotelId);
            var staffs = await _context.HotelStaffs
                .AsNoTracking()
                .Include(x => x.User)
                .Include(x => x.Position)
                .Where(x => x.HotelId == hotelId)
                .OrderByDescending(x => x.JoinedAt)
                .ToListAsync();

            return staffs
                .Select(x => x.ToResponse())
                .ToList();
        }


        public async Task<HotelStaffAttendanceResponse> GetAttendanceStaffByIdAsync(int userId, DateOnly? workDate = null)
        {
            var date = workDate ?? DateOnly.FromDateTime(DateTime.Today);

            return await _context.HotelStaffs
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .Select(HotelStaffMapper.ToAttendanceResponse(date))
                .FirstOrDefaultAsync()
                ?? throw new Exception("Không tìm thấy nhân viên.");
        }

        public async Task<List<WorkScheduleResponse>> GetMyWorkSchedulesAsync(int userId,int year,int month)
        {
            return await _context.WorkSchedules
                .AsNoTracking()
                .Where(x =>
                    x.HotelStaff.UserId == userId &&
                    x.WorkDate.Year == year &&
                    x.WorkDate.Month == month)
                .OrderBy(x => x.WorkDate)
                .Select(x => new WorkScheduleResponse
                {
                    Id = x.Id,
                    WorkDate = x.WorkDate,
                    IsDayOff = x.IsDayOff,

                    Shift = new ShiftReponse
                    {
                        Id = x.Shift.Id,
                        Name = x.Shift.Name,
                        StartTime = x.Shift.StartTime,
                        EndTime = x.Shift.EndTime
                    },

                    Attendance = x.Attendance == null
                        ? null
                        : new AttendanceResponse
                        {
                            Id = x.Attendance.Id,
                            CheckInTime = x.Attendance.CheckInTime,
                            CheckOutTime = x.Attendance.CheckOutTime,
                            Status = x.Attendance.Status,
                            Note = x.Attendance.Note
                        }
                })
                .ToListAsync();
        }
    }
}
