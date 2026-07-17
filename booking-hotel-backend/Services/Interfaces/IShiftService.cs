using booking_hotel_backend.Models.DTOs.Shift;

namespace booking_hotel_backend.Services.Interfaces
{
    public interface IShiftService
    {
        Task<List<ShiftReponse>> GetAllAsync();

        Task<ShiftReponse> GetByIdAsync(long id);

        Task<ShiftReponse> CreateAsync(CreateShiftRequest request);

        Task<ShiftReponse> UpdateAsync(long id, UpdateShiftRequest request);

        Task DeleteAsync(long id);
    }
}
