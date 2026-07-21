using booking_hotel_backend.Models.DTOs.StaffRequest;

namespace booking_hotel_backend.Services.Interfaces;

public interface IStaffRequestService
{
    Task<List<StaffRequestResponse>> GetAllAsync();
    Task<List<StaffRequestResponse>> GetByUserIdAsync(  int userId);
    Task DeleteAsync(long id);
    Task<StaffRequestResponse> GetByIdAsync(long id);
}