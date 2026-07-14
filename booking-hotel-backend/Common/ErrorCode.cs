namespace booking_hotel_backend.Common
{
    public class ErrorCode
    {
        public const string SUCCESS = "SUCCESS";

        // AUTH
        public const string AUTH_001 = "AUTH_001"; // Sai tài khoản
        public const string AUTH_002 = "AUTH_002"; // Tài khoản bị khóa
        public const string AUTH_003 = "AUTH_003"; // Token hết hạn
        public const string AUTH_004 = "AUTH_003";// Tài khoản đã kích hoạt
        // USER
        public const string USER_001 = "USER_001"; // Không tìm thấy
        public const string USER_002 = "USER_002"; // Email tồn tại

        // HOTEL
        public const string HOTEL_001 = "HOTEL_001";

        // SERVER
        public const string SERVER_001 = "SERVER_001";
    }
}
