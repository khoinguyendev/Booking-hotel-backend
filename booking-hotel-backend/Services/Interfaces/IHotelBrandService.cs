using booking_hotel_backend.Models.DTOs.Auth;
using booking_hotel_backend.Models.DTOs.HotelBrand;

namespace booking_hotel_backend.Services.Interfaces
{
    public interface IHotelBrandService
    {
        Task<HotelBrandResponse> CreateAsync(CreateHotelBrandRequest request);
        Task<HotelBrandResponse> UpdateAsync(long id, UpdateHotelBrandRequest request);
        Task<HotelBrandResponse> GetHotelAsync(long id);

    }
}
