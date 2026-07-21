using booking_hotel_backend.Models.DTOs.LeaveRequest;

namespace booking_hotel_backend.Services.Interfaces;

public interface ILeaveRequestService
{
    Task<List<LeaveRequestResponse>> GetAllAsync();

    Task<LeaveRequestResponse> GetByIdAsync(long id);
    Task<List<LeaveRequestResponse>> GetByStaffAsync(long hotelStaffId);
    Task<LeaveRequestResponse> CreateAsync(int userId,CreateLeaveRequestRequest request);

    Task<LeaveRequestResponse> UpdateAsync(long id, UpdateLeaveRequestRequest request);

    Task ApproveAsync(long id, long approvedBy);

    Task RejectAsync(long id, string rejectReason, long approvedBy);

    Task DeleteAsync(long id);
}