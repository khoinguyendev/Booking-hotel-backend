using booking_hotel_backend.Common;
using booking_hotel_backend.Models.DTOs.Attendance;
using booking_hotel_backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace booking_hotel_backend.Controllers;

[ApiController]
[Route("api/attendances")]
public class AttendanceController : ControllerBase
{
    private readonly IAttendanceService _service;

    public AttendanceController(IAttendanceService service)
    {
        _service = service;
    }

    [HttpPost("check-in")]
    public async Task<IActionResult> CheckIn(CheckInRequest request)
    {
        var data = await _service.CheckInAsync(request);

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Code = ErrorCode.SUCCESS,
            Message = "Check-in thành công.",
            Data = data
        });
    }

    [HttpPost("check-out")]
    public async Task<IActionResult> CheckOut(CheckOutRequest request)
    {
        var data = await _service.CheckOutAsync(request);

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Code = ErrorCode.SUCCESS,
            Message = "Check-out thành công.",
            Data = data
        });
    }
}