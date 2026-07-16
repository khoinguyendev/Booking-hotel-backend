using booking_hotel_backend.Models.DTOs.HotelBrand;
using booking_hotel_backend.Models.Entities;

namespace booking_hotel_backend.Extensions
{
    public static class HotelBrandMapper
    {
        public static HotelBrandResponse ToResponse(this HotelBrand brand)
        {
            return new HotelBrandResponse
            {
                Id = brand.Id,
                Name = brand.Name,
                Slug = brand.Slug,
                Logo = brand.Logo,
                Banner = brand.Banner,
                Description = brand.Description,
                Website = brand.Website,
                Phone = brand.Phone,
                Email = brand.Email,
                Status = brand.Status,
                CreatedAt = brand.CreatedAt,
                UpdatedAt = brand.UpdatedAt
            };
        }

        public static void UpdateFromRequest(this HotelBrand brand, UpdateHotelBrandRequest request)
        {
            if (request.Name != null)
                brand.Name = request.Name;

            if (request.Slug != null)
                brand.Slug = request.Slug;

            if (request.Logo != null)
                brand.Logo = request.Logo;

            if (request.Banner != null)
                brand.Banner = request.Banner;

            if (request.Description != null)
                brand.Description = request.Description;

            if (request.Website != null)
                brand.Website = request.Website;

            if (request.Phone != null)
                brand.Phone = request.Phone;

            if (request.Email != null)
                brand.Email = request.Email;

            if (request.Status.HasValue)
                brand.Status = request.Status.Value;

            brand.UpdatedAt = DateTime.UtcNow;
        }

        public static HotelBrand ToEntity(this CreateHotelBrandRequest request)
        {
            var brand = new HotelBrand
            {
                Name = request.Name,
                Slug = request.Slug,
                Logo = request.Logo,
                Banner = request.Banner,
                Description = request.Description,
                Website = request.Website,
                Phone = request.Phone,
                Email = request.Email,
                Status = request.Status,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            return brand;
        }
    }
}
