using booking_hotel_backend.Common;
using booking_hotel_backend.Models.Enums;

namespace booking_hotel_backend.Models.DTOs.User
{
    public class GetUsersRequest : PaginationRequest
    {
        public string? Keyword { get; set; }

        public Role? Role { get; set; }
    }
}
