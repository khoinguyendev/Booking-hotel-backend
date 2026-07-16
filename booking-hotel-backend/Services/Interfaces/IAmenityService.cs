using booking_hotel_backend.Models.DTOs.Amenity;

namespace booking_hotel_backend.Services.Interfaces;

public interface IAmenityService
{
    Task<List<AmenityResponse>> GetAllAsync();

    Task<AmenityResponse?> GetByIdAsync(long id);

    Task<AmenityResponse> CreateAsync(CreateAmenityRequest request);

    Task<AmenityResponse> UpdateAsync(long id, UpdateAmenityRequest request);

    Task DeleteAsync(long id);
}