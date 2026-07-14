using booking_hotel_backend.Common;
using booking_hotel_backend.Models.DTOs.Auth;
using booking_hotel_backend.Models.DTOs.User;

namespace booking_hotel_backend.Services.Interfaces
{
    public interface IUserService
    {
        Task<PagedResponse<UserResponse>> GetUsers(GetUsersRequest request);
    }
}
