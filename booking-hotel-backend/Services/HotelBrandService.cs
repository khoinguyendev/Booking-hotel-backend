using booking_hotel_backend.Data;
using booking_hotel_backend.Models.DTOs.HotelBrand;
using booking_hotel_backend.Models.Entities;
using booking_hotel_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using booking_hotel_backend.Extensions;
using booking_hotel_backend.Common.Exceptions;
namespace booking_hotel_backend.Services
{
    public class HotelBrandService : IHotelBrandService
    {
        private readonly AppDbContext _context;

        public HotelBrandService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<HotelBrandResponse> CreateAsync(CreateHotelBrandRequest request)
        {
            var exist = await _context.HotelBrands
                .FirstOrDefaultAsync(x => x.Slug == request.Slug);

            if (exist != null)
                throw new NotFoundException("404","Slug already exists");

            var brand = request.ToEntity();

            _context.HotelBrands.Add(brand);
            await _context.SaveChangesAsync();

            return brand.ToResponse();
        }

        public async Task<HotelBrandResponse> UpdateAsync(long id, UpdateHotelBrandRequest request)
        {
            var brand = await _context.HotelBrands.FindAsync(id)
                                ?? throw new NotFoundException("404","Brand not found");

            brand.UpdateFromRequest(request);

            await _context.SaveChangesAsync();

            return brand.ToResponse();

        }
        public async Task<HotelBrandResponse> GetHotelAsync(long id)
        {

            var brand = await _context.HotelBrands.FindAsync(id)
                                ?? throw new NotFoundException("404", "Brand not found");
            return brand.ToResponse();

        }

    }
}