using booking_hotel_backend.Models.DTOs.ShiftChangeRequest;

namespace booking_hotel_backend.Services.Interfaces;

public interface IShiftChangeRequestService
{
    Task<ShiftChangeRequestResponse> CreateAsync(int userId,CreateShiftChangeRequest request);

    Task<ShiftChangeRequestResponse> UpdateAsync(long id, UpdateShiftChangeRequest request);

    Task DeleteAsync(long id);

    Task<ShiftChangeRequestResponse> GetByIdAsync(long id);

    Task<List<ShiftChangeRequestResponse>> GetAllAsync();

    Task<ShiftChangeRequestResponse> ApproveAsync(long id, long managerId);

    Task<ShiftChangeRequestResponse> RejectAsync(long id, string reason, long managerId);
}