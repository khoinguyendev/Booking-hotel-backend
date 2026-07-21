using booking_hotel_backend.Common;
using booking_hotel_backend.Models.DTOs.Hotel;
using booking_hotel_backend.Models.DTOs.HotelStaff;

namespace booking_hotel_backend.Services.Interfaces
{
    public interface IHotellStaffService
    {
        Task CreateStaff(CreateHotelStaffRequest request);
        Task<PagedResponse<HotelStaffAttendanceResponse>> GetAttendanceStaffByManagerAsync(PaginationRequest request,int userId, DateOnly? workDate = null);
        Task<HotelStaffAttendanceResponse> GetAttendanceStaffByIdAsync(int id, DateOnly? workDate = null);
        Task<List<HotelStaffResponse>> GetStaffOfManagerAsync(long hotelId);
        Task<List<WorkScheduleResponse>> GetMyWorkSchedulesAsync(int userId, int year, int month);
        Task<object> Test();
    }
}
