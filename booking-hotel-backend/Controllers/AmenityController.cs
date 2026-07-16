using booking_hotel_backend.Common;
using booking_hotel_backend.Models.DTOs.Amenity;
using booking_hotel_backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace booking_hotel_backend.Controllers;

[ApiController]
[Route("api/amenities")]
public class AmenityController : ControllerBase
{
    private readonly IAmenityService _service;

    public AmenityController(IAmenityService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {

        return Ok(await _service.GetAllAsync());
    }

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

    [HttpPost]
    public async Task<IActionResult> Create(CreateAmenityRequest request)
    {
        var data = await _service.CreateAsync(request);

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Code = ErrorCode.SUCCESS,
            Message = "Tạo mới thành công.",
            Data = data
        });
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(long id, UpdateAmenityRequest request)
    {
        var data = await _service.UpdateAsync(id, request);

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Code = ErrorCode.SUCCESS,
            Message = "Cập nhật thành công.",
            Data = data
        });

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _service.DeleteAsync(id);
        return Ok(new ApiResponse<object>
        {
            Success = true,
            Code = ErrorCode.SUCCESS,
            Message = "Xóa thành công.",
        });
    }
}