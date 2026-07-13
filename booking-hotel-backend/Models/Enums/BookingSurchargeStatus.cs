namespace booking_hotel_backend.Models.Entities;

public enum BookingSurchargeStatus
{
    Pending = 1,    // Chờ khách sạn duyệt
    Approved = 2,   // Đã duyệt
    Rejected = 3,   // Từ chối
    Paid = 4,       // Đã thanh toán
    Cancelled = 5   // Hủy
}