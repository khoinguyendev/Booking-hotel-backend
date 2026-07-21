using booking_hotel_backend.Common;
using booking_hotel_backend.Models.DTOs.LeaveRequest;
using booking_hotel_backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace booking_hotel_backend.Controllers;

[ApiController]
[Route("api/leave-requests")]
public class LeaveRequestController : ControllerBase
{
    private readonly ILeaveRequestService _leaveRequestService;

    public LeaveRequestController(ILeaveRequestService leaveRequestService)
    {
        _leaveRequestService = leaveRequestService;
    }

    /// <summary>
    /// Danh sách đơn nghỉ phép
    /// </summary>
    //[Authorize(Roles = "Admin,Manager")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _leaveRequestService.GetAllAsync();

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Code = ErrorCode.SUCCESS,
            Message = "Lấy danh sách thành công.",
            Data = data
        });
    }

    /// <summary>
    /// Chi tiết đơn nghỉ phép
    /// </summary>
    [Authorize]
    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var data = await _leaveRequestService.GetByIdAsync(id);

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Code = ErrorCode.SUCCESS,
            Message = "Lấy dữ liệu thành công.",
            Data = data
        });
    }

    /// <summary>
    /// Nhân viên tạo đơn nghỉ
    /// </summary>
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create( CreateLeaveRequestRequest request)
    {
        var userId = int.Parse(
              User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var data = await _leaveRequestService.CreateAsync(userId,request);

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Code = ErrorCode.SUCCESS,
            Message = "Tạo đơn nghỉ thành công.",
            Data = data
        });
    }

    /// <summary>
    /// Sửa đơn nghỉ
    /// </summary>
    [Authorize]
    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, UpdateLeaveRequestRequest request)
    {
        var data = await _leaveRequestService.UpdateAsync(id, request);

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Code = ErrorCode.SUCCESS,
            Message = "Cập nhật thành công.",
            Data = data
        });
    }

    /// <summary>
    /// Duyệt đơn nghỉ
    /// </summary>
    [Authorize(Roles = "Admin,Manager")]
    [HttpPut("{id:long}/approve")]
    public async Task<IActionResult> Approve(long id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        await _leaveRequestService.ApproveAsync(id, userId);

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Code = ErrorCode.SUCCESS,
            Message = "Duyệt đơn thành công."
        });
    }

    /// <summary>
    /// Từ chối đơn nghỉ
    /// </summary>
    [Authorize(Roles = "Admin,Manager")]
    [HttpPut("{id:long}/reject")]
    public async Task<IActionResult> Reject(
        long id,
        RejectLeaveRequestRequest request)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        await _leaveRequestService.RejectAsync(
            id,
            request.Reason,
            userId);

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Code = ErrorCode.SUCCESS,
            Message = "Đã từ chối đơn."
        });
    }

    /// <summary>
    /// Xóa đơn nghỉ
    /// </summary>
    [Authorize]
    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _leaveRequestService.DeleteAsync(id);

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Code = ErrorCode.SUCCESS,
            Message = "Xóa thành công."
        });
    }
}