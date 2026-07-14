namespace booking_hotel_backend.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }

        public string Code { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public T? Data { get; set; }

        public object? Errors { get; set; }
    }
}
