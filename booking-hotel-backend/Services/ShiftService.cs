using booking_hotel_backend.Common.Exceptions;
using booking_hotel_backend.Data;
using booking_hotel_backend.Extensions;
using booking_hotel_backend.Models.DTOs.Hotel;
using booking_hotel_backend.Models.DTOs.Shift;
using booking_hotel_backend.Models.Entities;
using booking_hotel_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace booking_hotel_backend.Services
{
    public class ShiftService:IShiftService
    {
        private readonly AppDbContext _context;
        public ShiftService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ShiftReponse>> GetAllAsync()
        {
            var shifts = await _context.Shifts
                .ToListAsync();

            return shifts.Select(x => x.ToResponse()).ToList();
        }

        public async Task<ShiftReponse> GetByIdAsync(long id)
        {
            var shift = await _context.Shifts
               
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new Exception("Hotel not found.");

            return shift.ToResponse();
        }
        public async Task<List<ShiftReponse>> GetByHotelId(int userId)
        {
            var hotelStaff = await _context.HotelStaffs
        .FirstOrDefaultAsync(x => x.UserId == userId)
        ?? throw new BadRequestException("X001", "Nhân viên không tồn tại.");

            var shifts = await _context.Shifts
                .Where(x => x.HotelId == hotelStaff.HotelId && x.Status)
                .ToListAsync();

            return shifts.Select(x => x.ToResponse()).ToList();
        }

        public async Task<ShiftReponse> CreateAsync(CreateShiftRequest request)
        {
           

            
                var shift = request.ToEntity();

                _context.Shifts.Add(shift);

                await _context.SaveChangesAsync();
                return shift.ToResponse();

                
        }

        public async Task<ShiftReponse> UpdateAsync(long id, UpdateShiftRequest request)
        {
            var shift = await _context.Shifts
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new Exception("Shift not found.");

            shift.UpdateFromRequest(request);

            

            await _context.SaveChangesAsync();

            return shift.ToResponse();
        }

        public async Task DeleteAsync(long id)
        {
            var shift = await _context.Shifts
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new Exception("Hotel not found.");


            _context.Shifts.Remove(shift);

            await _context.SaveChangesAsync();
        }

       
    }
}
