using booking_hotel_backend.Common;
using booking_hotel_backend.Models.DTOs.HotelStaff;

namespace booking_hotel_backend.Services.Interfaces
{
    public interface IHotellStaffService
    {
        Task CreateStaff(CreateHotelStaffRequest request);
        Task<PagedResponse<HotelStaffResponse>> GetStaffByManagerAsync(PaginationRequest request,int userId, DateOnly? workDate = null);

        Task<object> Test();
    }
}
