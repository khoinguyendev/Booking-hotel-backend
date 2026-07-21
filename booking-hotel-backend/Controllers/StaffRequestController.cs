using booking_hotel_backend.Common;
using booking_hotel_backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace booking_hotel_backend.Controllers;

[ApiController]
[Route("api/staff-requests")]
public class StaffRequestController : ControllerBase
{
    private readonly IStaffRequestService _service;

    public StaffRequestController(IStaffRequestService service)
    {
        _service = service;
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpGet("attendance")]
    public async Task<IActionResult> GetAll()
    {
        var data = await _service.GetAllAsync();

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Code = ErrorCode.SUCCESS,
            Message = "Lấy dữ liệu thành công.",
            Data = data
        });
    }
    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetAllMe()
    {
        var userId = User.GetUserId();
        var data = await _service.GetByUserIdAsync(userId);
        
        return Ok(new ApiResponse<object>
        {
            Success = true,
            Code = ErrorCode.SUCCESS,
            Message = "Lấy dữ liệu thành công.",
            Data = data
        });
    }
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
       
       await _service.DeleteAsync(id);

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Code = ErrorCode.SUCCESS,
            Message = "Xóa thành công.",
            Data = null
        });
    }
    [Authorize(Roles = "Admin,Manager")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var data = await _service.GetByIdAsync(id);

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Code = ErrorCode.SUCCESS,
            Message = "Lấy dữ liệu thành công.",
            Data = data
        });
    }
}