using booking_hotel_backend.Models.DTOs.WorkSchedule;

namespace booking_hotel_backend.Services.Interfaces;

public interface IWorkScheduleService
{
    Task<List<WorkScheduleResponse>> GetAllAsync();

    Task<WorkScheduleResponse> GetByIdAsync(long id);

    Task<WorkScheduleResponse> CreateAsync(CreateWorkScheduleRequest request);

    Task<WorkScheduleResponse> UpdateAsync(long id, UpdateWorkScheduleRequest request);

    Task DeleteAsync(long id);
}