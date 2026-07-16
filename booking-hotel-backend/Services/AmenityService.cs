using booking_hotel_backend.Common.Exceptions;
using booking_hotel_backend.Data;
using booking_hotel_backend.Extensions;
using booking_hotel_backend.Models.DTOs.Amenity;
using booking_hotel_backend.Models.Entities;
using booking_hotel_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace booking_hotel_backend.Services;

public class AmenityService : IAmenityService
{
    private readonly AppDbContext _context;

    public AmenityService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<AmenityResponse>> GetAllAsync()
    {
        return await _context.Amenities
            .Select(x => x.ToResponse())
            .ToListAsync();
    }

    public async Task<AmenityResponse?> GetByIdAsync(long id)
    {
        var amenity = await _context.Amenities.FindAsync(id) ?? throw new NotFoundException("404","Amenity not found.");

        return amenity?.ToResponse();
    }

    public async Task<AmenityResponse> CreateAsync(CreateAmenityRequest request)
    {
        var exist = await _context.Amenities
            .AnyAsync(x => x.Name == request.Name);

        if (exist)
            throw new ConflictException("429","Amenity already exists.");

        var amenity = request.ToEntity();

        _context.Amenities.Add(amenity);

        await _context.SaveChangesAsync();

        return amenity.ToResponse();
    }

    public async Task<AmenityResponse> UpdateAsync(long id, UpdateAmenityRequest request)
    {
        var amenity = await _context.Amenities.FindAsync(id)
            ?? throw new NotFoundException("404","Amenity not found.");

        amenity.UpdateFromRequest(request);

        await _context.SaveChangesAsync();

        return amenity.ToResponse();
    }

    public async Task DeleteAsync(long id)
    {
        var amenity = await _context.Amenities.FindAsync(id)
            ?? throw new NotFoundException("404", "Amenity not found.");

        _context.Amenities.Remove(amenity);

        await _context.SaveChangesAsync();
    }
}