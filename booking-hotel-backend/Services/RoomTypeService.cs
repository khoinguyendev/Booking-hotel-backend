using booking_hotel_backend.Data;
using booking_hotel_backend.Models.DTOs.RoomType;
using booking_hotel_backend.Models.Entities;
using booking_hotel_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace booking_hotel_backend.Services;

public class RoomTypeService : IRoomTypeService
{
    private readonly AppDbContext _context;

    public RoomTypeService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<RoomType>> GetAll()
    {
        return await _context.RoomTypes
            .Include(x => x.Hotel)
            .ToListAsync();
    }

    public async Task<RoomType?> GetById(long id)
    {
        return await _context.RoomTypes
            .Include(x => x.Hotel)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<RoomType> Create(CreateRoomTypeRequest request)
    {
        var roomType = new RoomType
        {
            HotelId = request.HotelId,
            Name = request.Name,
            MaxGuest = request.MaxGuest,
            BedType = request.BedType,
            RoomSize = request.RoomSize,
            Description = request.Description
        };

        _context.RoomTypes.Add(roomType);
        await _context.SaveChangesAsync();

        return roomType;
    }

    public async Task<RoomType> Update(long id, UpdateRoomTypeRequest request)
    {
        var roomType = await _context.RoomTypes.FindAsync(id);

        if (roomType == null)
            throw new Exception("Room type not found");

        roomType.Name = request.Name;
        roomType.MaxGuest = request.MaxGuest;
        roomType.BedType = request.BedType;
        roomType.RoomSize = request.RoomSize;
        roomType.Description = request.Description;

        await _context.SaveChangesAsync();

        return roomType;
    }

    public async Task Delete(long id)
    {
        var roomType = await _context.RoomTypes.FindAsync(id);

        if (roomType == null)
            throw new Exception("Room type not found");

        _context.RoomTypes.Remove(roomType);

        await _context.SaveChangesAsync();
    }
}