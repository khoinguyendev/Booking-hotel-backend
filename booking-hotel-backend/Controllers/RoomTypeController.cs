using booking_hotel_backend.Common;
using booking_hotel_backend.Models.DTOs.RoomType;
using booking_hotel_backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace booking_hotel_backend.Controllers;

[ApiController]
[Route("api/room-types")]
public class RoomTypeController : ControllerBase
{
    private readonly IRoomTypeService _service;

    public RoomTypeController(IRoomTypeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _service.GetAll();

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Data = data
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var data = await _service.GetById(id);

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Data = data
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateRoomTypeRequest request)
    {
        var data = await _service.Create(request);

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Message = "Tạo thành công",
            Data = data
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, UpdateRoomTypeRequest request)
    {
        var data = await _service.Update(id, request);

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Message = "Cập nhật thành công",
            Data = data
        });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _service.Delete(id);

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Message = "Xóa thành công"
        });
    }
}