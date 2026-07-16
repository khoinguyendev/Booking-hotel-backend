using booking_hotel_backend.Data;
using booking_hotel_backend.Extensions;
using booking_hotel_backend.Models.DTOs.Hotel;
using booking_hotel_backend.Models.Entities;
using booking_hotel_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace booking_hotel_backend.Services;

public class HotelService : IHotelService
{
    private readonly AppDbContext _context;

    public HotelService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<HotelResponse>> GetAllAsync()
    {
        var hotels = await _context.Hotels
            .Include(x => x.Brand)
            .Include(x => x.HotelAmenities)
                .ThenInclude(x => x.Amenity)
            .ToListAsync();

        return hotels.Select(x => x.ToResponse()).ToList();
    }

    public async Task<HotelResponse> GetByIdAsync(long id)
    {
        var hotel = await _context.Hotels
            .Include(x => x.Brand)
            .Include(x => x.HotelAmenities)
                .ThenInclude(x => x.Amenity)
            .FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception("Hotel not found.");

        return hotel.ToResponse();
    }

    public async Task<HotelResponse> CreateAsync(CreateHotelRequest request)
    {
        await ValidateRequest(request);

        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var hotel = request.ToEntity();

            _context.Hotels.Add(hotel);

            await _context.SaveChangesAsync();

            if (request.AmenityIds.Any())
            {
                var hotelAmenities = request.AmenityIds
                    .Distinct()
                    .Select(x => new HotelAmenity
                    {
                        HotelId = hotel.Id,
                        AmenityId = x
                    });

                _context.HotelAmenities.AddRange(hotelAmenities);

                await _context.SaveChangesAsync();
            }

            await transaction.CommitAsync();

            return await GetByIdAsync(hotel.Id);
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<HotelResponse> UpdateAsync(long id, UpdateHotelRequest request)
    {
        var hotel = await _context.Hotels
            .Include(x => x.HotelAmenities)
            .FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception("Hotel not found.");

        hotel.UpdateFromRequest(request);

        if (request.AmenityIds != null)
        {
            _context.HotelAmenities.RemoveRange(hotel.HotelAmenities);

            var hotelAmenities = request.AmenityIds
                .Distinct()
                .Select(x => new HotelAmenity
                {
                    HotelId = hotel.Id,
                    AmenityId = x
                });

            await _context.HotelAmenities.AddRangeAsync(hotelAmenities);
        }

        await _context.SaveChangesAsync();

        return await GetByIdAsync(id);
    }

    public async Task DeleteAsync(long id)
    {
        var hotel = await _context.Hotels
            .Include(x => x.HotelAmenities)
            .FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception("Hotel not found.");

        _context.HotelAmenities.RemoveRange(hotel.HotelAmenities);

        _context.Hotels.Remove(hotel);

        await _context.SaveChangesAsync();
    }

    private async Task ValidateRequest(CreateHotelRequest request)
    {
        if (await _context.Hotels.AnyAsync(x => x.Slug == request.Slug))
            throw new Exception("Slug already exists.");

        if (!await _context.HotelBrands.AnyAsync(x => x.Id == request.BrandId))
            throw new Exception("Brand not found.");



        if (request.AmenityIds.Any())
        {
            var count = await _context.Amenities
                .CountAsync(x => request.AmenityIds.Contains(x.Id));

            if (count != request.AmenityIds.Count)
                throw new Exception("Amenity không hợp lệ.");
        }
    }
}