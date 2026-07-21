using booking_hotel_backend.Common;
using booking_hotel_backend.Models.DTOs.Hotel;
using booking_hotel_backend.Models.DTOs.Shift;
using booking_hotel_backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace booking_hotel_backend.Controllers;

[ApiController]
[Route("api/shifts")]
public class ShiftController : ControllerBase
{
    private readonly IShiftService _shiftService;

    public ShiftController(IShiftService shiftService)
    {
        _shiftService = shiftService;
    }

    /// <summary>
    /// Lấy danh sách khách sạn
    /// </summary>
    [Authorize("Roles = Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _shiftService.GetAllAsync();

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Code = ErrorCode.SUCCESS,
            Message = "Lấy danh sách thành công.",
            Data = data
        });
    }
    [Authorize]
    [HttpGet("hotel")]
    public async Task<IActionResult> GetByHotelId()
    {
        var userId = int.Parse(
         User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var data = await _shiftService.GetByHotelId(userId);

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Code = ErrorCode.SUCCESS,
            Message = "Lấy danh sách thành công.",
            Data = data
        });
    }
    /// <summary>
    /// Lấy chi tiết khách sạn
    /// </summary>
    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var data = await _shiftService.GetByIdAsync(id);

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Code = ErrorCode.SUCCESS,
            Message = "Lấy dữ liệu thành công.",
            Data = data
        });
    }

    /// <summary>
    /// Tạo khách sạn
    /// </summary>
    //[Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateShiftRequest request)
    {
        var data = await _shiftService.CreateAsync(request);

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Code = ErrorCode.SUCCESS,
            Message = "Tạo khách sạn thành công.",
            Data = data
        });
    }

    /// <summary>
    /// Cập nhật khách sạn
    /// </summary>
    //[Authorize(Roles = "Admin")]
    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, UpdateShiftRequest request)
    {
        var data = await _shiftService.UpdateAsync(id, request);

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Code = ErrorCode.SUCCESS,
            Message = "Cập nhật khách sạn thành công.",
            Data = data
        });
    }

    /// <summary>
    /// Xóa khách sạn
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _shiftService.DeleteAsync(id);

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Code = ErrorCode.SUCCESS,
            Message = "Xóa khách sạn thành công."
        });
    }
}