using booking_hotel_backend.Common;
using booking_hotel_backend.Helpers;
using booking_hotel_backend.Models.DTOs.ShiftChangeRequest;
using booking_hotel_backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace booking_hotel_backend.Controllers;

[ApiController]
[Route("api/shift-change-requests")]
[Authorize]
public class ShiftChangeRequestController : ControllerBase
{
    private readonly IShiftChangeRequestService _service;

    public ShiftChangeRequestController(IShiftChangeRequestService service)
    {
        _service = service;
    }

    /// <summary>
    /// Nhân viên gửi đơn đổi ca
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(CreateShiftChangeRequest request)
    {
        var userId = int.Parse(
         User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var data = await _service.CreateAsync(userId,request);

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Code = ErrorCode.SUCCESS,
            Message = "Tạo đơn đổi ca thành công.",
            Data = data
        });
    }

    /// <summary>
    /// Nhân viên sửa đơn khi chưa duyệt
    /// </summary>
    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, UpdateShiftChangeRequest request)
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

    /// <summary>
    /// Xóa đơn
    /// </summary>
    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _service.DeleteAsync(id);

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Code = ErrorCode.SUCCESS,
            Message = "Xóa thành công."
        });
    }

    /// <summary>
    /// Chi tiết đơn
    /// </summary>
    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var data = await _service.GetByIdAsync(id);

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Code = ErrorCode.SUCCESS,
            Data = data
        });
    }

    /// <summary>
    /// Danh sách đơn
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _service.GetAllAsync();

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Code = ErrorCode.SUCCESS,
            Data = data
        });
    }

    /// <summary>
    /// Manager duyệt đơn
    /// </summary>
    [HttpPut("{id:long}/approve")]
    public async Task<IActionResult> Approve(long id, [FromQuery] long managerId)
    {
        var data = await _service.ApproveAsync(id, managerId);

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Code = ErrorCode.SUCCESS,
            Message = "Duyệt đơn thành công.",
            Data = data
        });
    }

    /// <summary>
    /// Manager từ chối
    /// </summary>
    //[HttpPut("{id:long}/reject")]
    //public async Task<IActionResult> Reject(
    //    long id,
    //    [FromQuery] long managerId,
    //    [FromBody] RejectShiftChangeRequest request)
    //{
    //    var data = await _service.RejectAsync(id, request.Reason, managerId);

    //    return Ok(new ApiResponse<object>
    //    {
    //        Success = true,
    //        Code = ErrorCode.SUCCESS,
    //        Message = "Đã từ chối đơn.",
    //        Data = data
    //    });
    //}
}