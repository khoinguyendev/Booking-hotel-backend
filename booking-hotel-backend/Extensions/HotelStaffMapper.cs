using booking_hotel_backend.Models.DTOs.Attendance;
using booking_hotel_backend.Models.DTOs.Hotel;
using booking_hotel_backend.Models.DTOs.HotelStaff;
using booking_hotel_backend.Models.DTOs.Shift;
using booking_hotel_backend.Models.Entities;
using System.Linq.Expressions;

namespace booking_hotel_backend.Extensions
{
    public static class HotelStaffMapper
    {
        public static HotelStaff ToEntity(this CreateHotelStaffRequest request,int userId)
        {
            return new HotelStaff
            {
                UserId = userId,
                HotelId = request.HotelId,
                PositionId = request.PositionId,
                EmployeeCode = request.CodeId,
            };
        }

        public static User ToEntity(this CreateHotelStaffRequest request)
        {
            return new User
            {
                FullName = request.FullName,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = request.Role,
            };
        }

        public static Expression<Func<HotelStaff, HotelStaffAttendanceResponse>> ToAttendanceResponse(DateOnly workDate)
        {
            return x => new HotelStaffAttendanceResponse
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
                    .Where(w => w.WorkDate == workDate)
                    .Select(w => new WorkScheduleResponse
                    {
                        Id = w.Id,
                        WorkDate = w.WorkDate,
                        IsDayOff = w.IsDayOff,

                        Shift = new ShiftReponse
                        {
                            Id = w.Shift.Id,
                            Name = w.Shift.Name,
                            StartTime = w.Shift.StartTime,
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
            };
        }

        public static HotelStaffResponse ToResponse(this HotelStaff hotelStaff)
        {
            return new HotelStaffResponse
            {
                Id = hotelStaff.Id,
                UserId = hotelStaff.UserId,
                FullName = hotelStaff.User.FullName,
                Email = hotelStaff.User.Email,
                Phone = hotelStaff.User.Phone,
                EmployeeCode = hotelStaff.EmployeeCode,
                Avatar = hotelStaff.User.Avatar,
                Position = hotelStaff.Position.Name,
                JoinedAt = hotelStaff.JoinedAt,
                Status = hotelStaff.Status
            };
        }
       

    }
}
