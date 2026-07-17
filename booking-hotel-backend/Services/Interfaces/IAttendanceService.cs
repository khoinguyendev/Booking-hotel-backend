using booking_hotel_backend.Models.DTOs.Attendance;

namespace booking_hotel_backend.Services.Interfaces
{
    public interface IAttendanceService
    {
        Task<AttendanceResponse> CheckInAsync(CheckInRequest request);

        Task<AttendanceResponse> CheckOutAsync(CheckOutRequest request);
    }
}
