using booking_hotel_backend.Common;
using booking_hotel_backend.Data;
using booking_hotel_backend.Extensions;
using booking_hotel_backend.Models.DTOs.Attendance;
using booking_hotel_backend.Models.DTOs.HotelStaff;
using booking_hotel_backend.Models.DTOs.Shift;
using booking_hotel_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace booking_hotel_backend.Services
{
    public class HotelStaffService:IHotellStaffService
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
        public async Task<PagedResponse<HotelStaffResponse>> GetStaffByManagerAsync(
     PaginationRequest request,
     int userId, DateOnly? workDate = null)
        {
            var manager = await _context.HotelStaffs
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (manager == null)
                throw new Exception("Bạn không thuộc khách sạn nào.");
            var date = workDate ?? DateOnly.FromDateTime(DateTime.Today);
            Console.WriteLine($"workDate = {workDate}");
            Console.WriteLine($"date = {date}");
            Console.WriteLine($"Today = {DateOnly.FromDateTime(DateTime.Today)}");
            var query = _context.HotelStaffs
                .AsNoTracking()
                .Where(x => x.HotelId == manager.HotelId)
                .OrderByDescending(x => x.JoinedAt)
                .Select(x => new HotelStaffResponse
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    FullName = x.User.FullName,
                    Email = x.User.Email,
                    Phone = x.User.Phone,
                    EmployeeCode = x.EmployeeCode,
                    Avatar = x.User.Avatar,
                    Position = x.Position.Name,
                    JoinedAt = x.JoinedAt,
                    Status = x.Status,

                    WorkSchedule = x.WorkSchedules
                        .Where(w => w.WorkDate == date)
                        .Select(w => new WorkScheduleResponse
                        {
                            Id = w.Id,
                            WorkDate = w.WorkDate,
                            IsDayOff = w.IsDayOff,

                            Shift = new ShiftReponse
                            {
                                Id = w.Shift.Id,
                                Name = w.Shift.Name,
                                StartTime= w.Shift.StartTime,
                                EndTime = w.Shift.EndTime
                            },

                            Attendance = w.Attendance == null
                                ? null
                                : new AttendanceResponse
                                {
                                    Id = w.Attendance.Id,
                                    CheckInTime = w.Attendance.CheckInTime,
                                    CheckOutTime = w.Attendance.CheckOutTime,
                                    Status = w.Attendance.Status,
                                    Note = w.Attendance.Note
                                }
                        })
                        .FirstOrDefault()
                });

            return await query.ToPagedResponseAsync(
                request.Page,
                request.PageSize);
        }
    }
}
