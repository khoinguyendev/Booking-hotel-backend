using booking_hotel_backend.Common;
using booking_hotel_backend.Models.DTOs.Hotel;
using booking_hotel_backend.Models.DTOs.HotelStaff;
using booking_hotel_backend.Models.DTOs.User;
using booking_hotel_backend.Services;
using booking_hotel_backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace booking_hotel_backend.Controllers
{
    [ApiController]
    [Route("api/hotel-staffs")]
    public class HotelStaffController:ControllerBase
    {
        private readonly IHotellStaffService _hotelStaffService;

        public HotelStaffController(IHotellStaffService hotelService)
        {
            _hotelStaffService = hotelService;
        }
        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateHotelStaffRequest request)
        {
            await _hotelStaffService.CreateStaff(request);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Code = ErrorCode.SUCCESS,
                Message = "Them thành công.",
            });
        }
        [Authorize(Roles = "Manager")]
        [HttpGet]
        public async Task<IActionResult> GetStaffs([FromQuery] PaginationRequest request, [FromQuery] DateOnly? date=null)
        {
            var userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var data = await _hotelStaffService.GetStaffByManagerAsync(request,userId,date);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Code = ErrorCode.SUCCESS,
                Message = "Lấy danh sách nhân viên thành công.",
                Data = data
            });
        }

        [HttpGet("/test")]
        public async Task<IActionResult> Test()
        {
         

            var data = await _hotelStaffService.Test();

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Code = ErrorCode.SUCCESS,
                Message = "Lấy danh sách nhân viên thành công.",
                Data = data
            });
        }
    }
}
