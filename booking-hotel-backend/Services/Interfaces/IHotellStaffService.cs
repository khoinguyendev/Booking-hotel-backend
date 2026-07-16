using booking_hotel_backend.Models.DTOs.HotelStaff;

namespace booking_hotel_backend.Services.Interfaces
{
    public interface IHotellStaffService
    {
        Task CreateStaff(CreateHotelStaffRequest request);
        Task<List<HotelStaffResponse>> GetStaffByManagerAsync(int userId);
    }
}
