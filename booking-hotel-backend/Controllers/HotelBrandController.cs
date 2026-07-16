using booking_hotel_backend.Common;
using booking_hotel_backend.Models.DTOs.HotelBrand;
using booking_hotel_backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace booking_hotel_backend.Controllers;

[ApiController]
[Route("api/hotel-brands")]
public class HotelBrandController : ControllerBase
{
    private readonly IHotelBrandService _service;

    public HotelBrandController(IHotelBrandService service)
    {
        _service = service;
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> Get(long id)
    {
        var brand = await _service.GetHotelAsync(id);

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Code = ErrorCode.SUCCESS,
            Message = "Lấy dữ liệu thành công.",
            Data = brand
        });
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateHotelBrandRequest request)
    {
        var brand = await _service.CreateAsync(request);
        return Ok(new ApiResponse<object>
        {
            Success = true,
            Code = ErrorCode.SUCCESS,
            Message = "Tạo thành công.",
            Data = brand
        });
    }
    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(long id, UpdateHotelBrandRequest request)
    {
        var brand = await _service.UpdateAsync(id, request);

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Code = ErrorCode.SUCCESS,
            Message = "Cập nhật thành công.",
            Data = brand
        });
    }

}