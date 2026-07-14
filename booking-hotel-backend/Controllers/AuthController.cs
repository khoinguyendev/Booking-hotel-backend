using booking_hotel_backend.Common;
using booking_hotel_backend.Models.DTOs.Auth;
using booking_hotel_backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace booking_hotel_backend.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("employee/login")]
        public async Task<IActionResult> EmployeeLogin(EmployeeLoginRequest request)
        {
            var result = await _authService.EmployeeLogin(request);

            return Ok(new ApiResponse<LoginResponse>
            {
                Success = true,
                Code = ErrorCode.SUCCESS,
                Message = "Đăng nhập thành công",
                Data = result
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            await _authService.Register(request);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Code = ErrorCode.SUCCESS,
                Message = "Đăng ký thành công. Vui lòng kiểm tra email để lấy mã OTP.",
                Data = null
            });
        }
        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail(VerifyEmailRequest request)
        {
             await _authService.VerifyEmail(request);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Code = ErrorCode.SUCCESS,
                Message = "Email đã xác thực.",
                Data = null
            });
        }
        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            return Ok(new
            {
                message = "thành công."
            });
        }

    }
}