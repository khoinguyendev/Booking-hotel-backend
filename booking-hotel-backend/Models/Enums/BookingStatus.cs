namespace booking_hotel_backend.Models.Entities;

public enum BookingStatus
{
    Pending = 1,      // Chờ xác nhận
    Confirmed = 2,    // Đã xác nhận
    CheckedIn = 3,    // Đã nhận phòng
    CheckedOut = 4,   // Đã trả phòng
    Cancelled = 5,    // Đã hủy
    NoShow = 6        // Không đến
}