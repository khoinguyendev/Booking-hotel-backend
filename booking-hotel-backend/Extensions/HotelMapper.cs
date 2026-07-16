using booking_hotel_backend.Models.DTOs.Amenity;
using booking_hotel_backend.Models.DTOs.Hotel;
using booking_hotel_backend.Models.Entities;

namespace booking_hotel_backend.Extensions;

public static class HotelMapper
{
    public static Hotel ToEntity(this CreateHotelRequest request)
    {
        return new Hotel
        {
            BrandId = request.BrandId,
            City = request.City,
            Name = request.Name,
            Slug = request.Slug,
            Image = request.Image ?? "",
            Description = request.Description,
            Address = request.Address,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            Phone = request.Phone,
            Email = request.Email,
            Star = request.Star,
            CheckinTime = request.CheckinTime,
            CheckoutTime = request.CheckoutTime,
            Status = request.Status,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public static void UpdateFromRequest(this Hotel hotel, UpdateHotelRequest request)
    {
        hotel.BrandId = request.BrandId ?? hotel.BrandId;
        hotel.City = request.City ?? hotel.City;
        hotel.Name = request.Name ?? hotel.Name;
        hotel.Slug = request.Slug ?? hotel.Slug;
        hotel.Image = request.Image ?? hotel.Image;
        hotel.Description = request.Description ?? hotel.Description;
        hotel.Address = request.Address ?? hotel.Address;
        hotel.Latitude = request.Latitude ?? hotel.Latitude;
        hotel.Longitude = request.Longitude ?? hotel.Longitude;
        hotel.Phone = request.Phone ?? hotel.Phone;
        hotel.Email = request.Email ?? hotel.Email;
        hotel.Star = request.Star ?? hotel.Star;
        hotel.CheckinTime = request.CheckinTime ?? hotel.CheckinTime;
        hotel.CheckoutTime = request.CheckoutTime ?? hotel.CheckoutTime;
        hotel.Status = request.Status ?? hotel.Status;
        hotel.UpdatedAt = DateTime.UtcNow;
    }

    public static HotelResponse ToResponse(this Hotel hotel)
    {
        return new HotelResponse
        {
            Id = hotel.Id,
            BrandId = hotel.BrandId,
            BrandName = hotel.Brand?.Name ?? "",
            City = hotel.City,
            Name = hotel.Name,
            Slug = hotel.Slug,
            Image = hotel.Image,
            Description = hotel.Description,
            Address = hotel.Address,
            Latitude = hotel.Latitude,
            Longitude = hotel.Longitude,
            Phone = hotel.Phone,
            Email = hotel.Email,
            Star = hotel.Star,
            CheckinTime = hotel.CheckinTime,
            CheckoutTime = hotel.CheckoutTime,
            Status = hotel.Status,
            CreatedAt = hotel.CreatedAt,
            UpdatedAt = hotel.UpdatedAt,
            Amenities = hotel.HotelAmenities
                .Select(x => new AmenityResponse
                {
                    Id = x.Amenity.Id,
                    Name = x.Amenity.Name,
                    Icon = x.Amenity.Icon
                })
                .ToList()
        };
    }
}