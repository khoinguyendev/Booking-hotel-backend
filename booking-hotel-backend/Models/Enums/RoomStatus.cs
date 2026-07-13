namespace booking_hotel_backend.Models.Entities;

public enum RoomStatus
{
    Available = 1,      // Sẵn sàng cho thuê
    Occupied = 2,       // Đang có khách
    Reserved = 3,       // Đã được đặt
    Cleaning = 4,       // Đang dọn phòng
    Maintenance = 5,    // Đang bảo trì
    OutOfService = 6    // Ngừng sử dụng
}