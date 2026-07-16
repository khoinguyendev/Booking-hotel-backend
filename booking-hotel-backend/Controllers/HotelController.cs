using booking_hotel_backend.Common;
using booking_hotel_backend.Models.DTOs.Hotel;
using booking_hotel_backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace booking_hotel_backend.Controllers;

[ApiController]
[Route("api/hotels")]
public class HotelController : ControllerBase
{
    private readonly IHotelService _hotelService;

    public HotelController(IHotelService hotelService)
    {
        _hotelService = hotelService;
    }

    /// <summary>
    /// Lấy danh sách khách sạn
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _hotelService.GetAllAsync();

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
        var data = await _hotelService.GetByIdAsync(id);

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
    public async Task<IActionResult> Create(CreateHotelRequest request)
    {
        var data = await _hotelService.CreateAsync(request);

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
    public async Task<IActionResult> Update(long id, UpdateHotelRequest request)
    {
        var data = await _hotelService.UpdateAsync(id, request);

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
        await _hotelService.DeleteAsync(id);

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Code = ErrorCode.SUCCESS,
            Message = "Xóa khách sạn thành công."
        });
    }
}