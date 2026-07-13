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

            if (result == null)
            {
                return Unauthorized(new
                {
                    message = "CodeId hoặc Password không đúng."
                });
            }

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _authService.Register(request);

            if (!result)
            {
                return BadRequest(new
                {
                    message = "Email đã tồn tại."
                });
            }

            return Ok(new
            {
                message = "Đăng ký thành công."
            });
        }
    }
}