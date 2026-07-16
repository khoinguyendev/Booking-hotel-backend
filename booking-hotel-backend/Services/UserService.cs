using booking_hotel_backend.Common;
using booking_hotel_backend.Data;
using booking_hotel_backend.Extensions;
using booking_hotel_backend.Models.DTOs.Auth;
using booking_hotel_backend.Models.DTOs.HotelStaff;
using booking_hotel_backend.Models.DTOs.User;
using booking_hotel_backend.Models.Entities;
using booking_hotel_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace booking_hotel_backend.Services
{
    public class UserService:IUserService
    {
        private readonly AppDbContext _context;
        public UserService(
           AppDbContext context
      )
        {
            _context = context;
 
        }
        public async Task<PagedResponse<UserResponse>> GetUsers(GetUsersRequest request)
        {
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Keyword))
            {
                query = query.Where(x =>
                    x.FullName.Contains(request.Keyword) ||
                    x.Email.Contains(request.Keyword));
            }

            if (request.Role.HasValue)
            {
                query = query.Where(x => x.Role == request.Role.Value);
            }

            var totalItems = await query.CountAsync();

            var users = await query
                .OrderByDescending(x => x.CreatedAt)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return new PagedResponse<UserResponse>
            {
                Items = users.Select(x => x.ToResponse()).ToList(),
                Page = request.Page,
                PageSize = request.PageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling((double)totalItems / request.PageSize)
            };
        }

        public async Task<UserResponse> CreateManager(CreateUserRequest request)
        {
            var user = new Models.Entities.User
            {
                FullName = request.FullName,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = request.Role,
                CodeId = request.CodeId,
                IsActive = true
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user.ToResponse();
        }

        
    }
}
