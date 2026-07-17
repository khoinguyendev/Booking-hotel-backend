using booking_hotel_backend.Models.DTOs.Hotel;
using booking_hotel_backend.Models.DTOs.HotelStaff;
using booking_hotel_backend.Models.Entities;

namespace booking_hotel_backend.Extensions
{
    public static class HotelStaffMapper
    {
        public static HotelStaff ToEntity(this CreateHotelStaffRequest request,int userId)
        {
            return new HotelStaff
            {
                UserId = userId,
                HotelId = request.HotelId,
                PositionId = request.PositionId,
                EmployeeCode = request.CodeId,
            };
        }

        public static User ToEntity(this CreateHotelStaffRequest request)
        {
            return new User
            {
                FullName = request.FullName,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = request.Role,
            };
        }
    }
}
