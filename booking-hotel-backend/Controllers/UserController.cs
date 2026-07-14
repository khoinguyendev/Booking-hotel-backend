using booking_hotel_backend.Common;
using booking_hotel_backend.Models.DTOs.Auth;
using booking_hotel_backend.Models.DTOs.User;

using Microsoft.AspNetCore.Mvc;
using booking_hotel_backend.Services.Interfaces;

namespace booking_hotel_backend.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController: ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers(
    [FromQuery] GetUsersRequest request)
        {
            var result = await _userService.GetUsers(request);

            return Ok(new ApiResponse<PagedResponse<UserResponse>>
            {
                Success = true,
                Code = ErrorCode.SUCCESS,
                Message = "Lấy danh sách người dùng thành công.",
                Data = result
            });
        }
    }
}
