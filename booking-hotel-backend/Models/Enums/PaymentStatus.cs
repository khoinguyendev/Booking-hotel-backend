namespace booking_hotel_backend.Models.Entities;

public enum PaymentStatus
{
    Unpaid = 1,        // Chưa thanh toán
    PartiallyPaid = 2, // Đã cọc / thanh toán một phần
    Paid = 3,          // Đã thanh toán
    Refunded = 4       // Đã hoàn tiền
}