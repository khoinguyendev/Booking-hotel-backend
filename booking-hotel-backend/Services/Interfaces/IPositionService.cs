using booking_hotel_backend.Models.DTOs.Position;

namespace booking_hotel_backend.Services.Interfaces
{
    public interface IPositionService
    {
        Task<List<PositionResponse>> GetAllAsync();

        Task<PositionResponse> GetByIdAsync(long id);

        Task<PositionResponse> CreateAsync(CreatePositionRequest request);

        Task<PositionResponse> UpdateAsync(long id, UpdatePositionRequest request);

        Task DeleteAsync(long id);
    }
}