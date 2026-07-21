using booking_hotel_backend.Common;
using booking_hotel_backend.Models.DTOs.Hotel;
using booking_hotel_backend.Models.DTOs.HotelStaff;
using booking_hotel_backend.Models.DTOs.User;
using booking_hotel_backend.Models.Entities;
using booking_hotel_backend.Services;
using booking_hotel_backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
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
                Message = "Thêm thành công.",
            });
        }
        [Authorize(Roles = "Manager")]
        [HttpGet("attendance")]
        public async Task<IActionResult> GetAttendanceStaffByManagerAsync([FromQuery] PaginationRequest request, [FromQuery] DateOnly? date=null)
        {
            var userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var data = await _hotelStaffService.GetAttendanceStaffByManagerAsync(request,userId,date);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Code = ErrorCode.SUCCESS,
                Message = "Lấy danh sách nhân viên thành công.",
                Data = data
            });
        }
        [Authorize(Roles = "Manager")]
        [HttpGet]
        public async Task<IActionResult> GetStaffOfManagerAsync()
        {
           
            var hotelId = User.GetHotelId();

            var data = await _hotelStaffService.GetStaffOfManagerAsync(hotelId);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Code = ErrorCode.SUCCESS,
                Message = "Lấy danh sách nhân viên thành công.",
                Data = data
            });
        }
        [Authorize(Roles = "Manager")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetStaff(int id,[FromQuery] DateOnly? date = null)
        {
            var data = await _hotelStaffService.GetAttendanceStaffByIdAsync(id, date);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Code = ErrorCode.SUCCESS,
                Message = "Lấy dữ liệu thành công.",
                Data = data
            });
        }
        [Authorize(Roles = "Manager")]
        [HttpGet("{userId:int}/work-schedules")]
        public async Task<IActionResult> GetMyWorkSchedules(
            int userId, [FromQuery] int year, [FromQuery] int month)
        {

            var data = await _hotelStaffService.GetMyWorkSchedulesAsync(userId, year, month);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Code = ErrorCode.SUCCESS,
                Message = "Lấy lịch làm việc thành công.",
                Data = data
            });
        }
        [Authorize(Roles = "Staff")]
        [HttpGet("me/work-schedules")]
        public async Task<IActionResult> GetWorkSchedules(
    [FromQuery] int year,
    [FromQuery] int month)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var data = await _hotelStaffService.GetMyWorkSchedulesAsync(userId, year, month);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Code = ErrorCode.SUCCESS,
                Message = "Lấy lịch làm việc thành công.",
                Data = data
            });
        }
        [Authorize(Roles = "Staff")]
        [HttpGet("me")]
        public async Task<IActionResult> GetMe([FromQuery] DateOnly? date = null)
        {
            var userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var data = await _hotelStaffService.GetAttendanceStaffByIdAsync(userId, date);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Code = ErrorCode.SUCCESS,
                Message = "Lấy dữ liệu thành công.",
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
