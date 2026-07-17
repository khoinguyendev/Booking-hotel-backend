using booking_hotel_backend.Data;
using booking_hotel_backend.Models.DTOs.Position;
using booking_hotel_backend.Models.Entities;
using booking_hotel_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace booking_hotel_backend.Services
{
    public class PositionService : IPositionService
    {
        private readonly AppDbContext _context;

        public PositionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<PositionResponse>> GetAllAsync()
        {
            return await _context.Positions
                .Select(x => new PositionResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Status = x.Status,
                    CreatedAt = x.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<PositionResponse> GetByIdAsync(long id)
        {
            var position = await _context.Positions.FindAsync(id);

            if (position == null)
                throw new Exception("Không tìm thấy chức vụ.");

            return new PositionResponse
            {
                Id = position.Id,
                Name = position.Name,
                Description = position.Description,
                Status = position.Status,
                CreatedAt = position.CreatedAt
            };
        }

        public async Task<PositionResponse> CreateAsync(CreatePositionRequest request)
        {
            if (await _context.Positions.AnyAsync(x => x.Name == request.Name))
                throw new Exception("Tên chức vụ đã tồn tại.");

            var position = new Position
            {
                Name = request.Name,
                Description = request.Description,
                Status = request.Status
            };

            _context.Positions.Add(position);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(position.Id);
        }

        public async Task<PositionResponse> UpdateAsync(long id, UpdatePositionRequest request)
        {
            var position = await _context.Positions.FindAsync(id);

            if (position == null)
                throw new Exception("Không tìm thấy chức vụ.");

            if (await _context.Positions.AnyAsync(x =>
                x.Name == request.Name && x.Id != id))
            {
                throw new Exception("Tên chức vụ đã tồn tại.");
            }

            position.Name = request.Name;
            position.Description = request.Description;
            position.Status = request.Status;

            await _context.SaveChangesAsync();

            return await GetByIdAsync(id);
        }

        public async Task DeleteAsync(long id)
        {
            var position = await _context.Positions.FindAsync(id);

            if (position == null)
                throw new Exception("Không tìm thấy chức vụ.");

            _context.Positions.Remove(position);

            await _context.SaveChangesAsync();
        }
    }
}