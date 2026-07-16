using booking_hotel_backend.Models.DTOs.Hotel;

namespace booking_hotel_backend.Services.Interfaces;

public interface IHotelService
{
    Task<List<HotelResponse>> GetAllAsync();

    Task<HotelResponse> GetByIdAsync(long id);

    Task<HotelResponse> CreateAsync(CreateHotelRequest request);

    Task<HotelResponse> UpdateAsync(long id, UpdateHotelRequest request);

    Task DeleteAsync(long id);
}