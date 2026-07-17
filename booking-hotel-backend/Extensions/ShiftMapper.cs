using booking_hotel_backend.Models.DTOs.Amenity;
using booking_hotel_backend.Models.DTOs.Hotel;
using booking_hotel_backend.Models.DTOs.Shift;
using booking_hotel_backend.Models.Entities;

namespace booking_hotel_backend.Extensions
{
    public static class ShiftMapper
    {
        public static Shift ToEntity(this CreateShiftRequest request)
        {
            return new Shift
            {
                HotelId = request.HotelId,
                Name = request.Name,
                StartTime = request.StartTime,
                EndTime = request.EndTime,

            };
        }

        public static void UpdateFromRequest(this Shift shift, UpdateShiftRequest request)
        {
            shift.HotelId = request.HotelId ?? shift.HotelId;
            shift.Name = request.Name ?? shift.Name;
            shift.StartTime = request.StartTime ?? shift.StartTime;
            shift.EndTime = request.EndTime ?? shift.EndTime;

        }

        public static ShiftReponse ToResponse(this Shift shift)
        {
            return new ShiftReponse
            {
                Id = shift.Id,
                Name = shift.Name,
                StartTime = shift.StartTime,
                EndTime = shift.EndTime,

            };
        }
    }
}
