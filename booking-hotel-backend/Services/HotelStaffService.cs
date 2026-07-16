using booking_hotel_backend.Data;
using booking_hotel_backend.Models.DTOs.HotelStaff;
using booking_hotel_backend.Models.Entities;
using booking_hotel_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace booking_hotel_backend.Services
{
    public class HotelStaffService:IHotellStaffService
    {
        private readonly AppDbContext _context;
        public HotelStaffService(
           AppDbContext context
      )
        {
            _context = context;

        }

        public async Task CreateStaff(CreateHotelStaffRequest request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var user = new User
                {
                    FullName = request.FullName,
                    Email = request.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    Role = request.Role,
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                var hotelStaff = new HotelStaff
                {
                    UserId = user.Id,
                    HotelId = request.HotelId,
                    PositionId = request.PositionId
                };

                _context.HotelStaffs.Add(hotelStaff);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<List<HotelStaffResponse>> GetStaffByManagerAsync(int userId)
        {
            // Lấy bản ghi của manager trong hotel_staffs
            var manager = await _context.HotelStaffs
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (manager == null)
                throw new Exception("Bạn không thuộc khách sạn nào.");

            // Lấy toàn bộ nhân viên của khách sạn
            var staffs = await _context.HotelStaffs
                .Where(x => x.HotelId == manager.HotelId)
                .Include(x => x.User)
                .Include(x => x.Position)
                .Select(x => new HotelStaffResponse
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    FullName = x.User.FullName,
                    Email = x.User.Email,
                    Phone = x.User.Phone,
                    Avatar = x.User.Avatar,
                    Position = x.Position.Name,
                    JoinedAt = x.JoinedAt,
                    Status = x.Status
                })
                .ToListAsync();

            return staffs;
        }
    }
}
