using booking_hotel_backend.Models.DTOs.Amenity;
using booking_hotel_backend.Models.Entities;

namespace booking_hotel_backend.Extensions;

public static class AmenityMapper
{
    public static AmenityResponse ToResponse(this Amenity amenity)
    {
        return new AmenityResponse
        {
            Id = amenity.Id,
            Name = amenity.Name,
            Icon = amenity.Icon
        };
    }

    public static Amenity ToEntity(this CreateAmenityRequest request)
    {
        return new Amenity
        {
            Name = request.Name,
            Icon = request.Icon
        };
    }

    public static void UpdateFromRequest(this Amenity amenity, UpdateAmenityRequest request)
    {
        if (request.Name != null)
            amenity.Name = request.Name;

        if (request.Icon != null)
            amenity.Icon = request.Icon;
    }
}