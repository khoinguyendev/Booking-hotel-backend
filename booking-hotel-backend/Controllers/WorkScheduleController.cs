using booking_hotel_backend.Common;
using booking_hotel_backend.Models.DTOs.WorkSchedule;
using booking_hotel_backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace booking_hotel_backend.Controllers;

[ApiController]
[Route("api/work-schedules")]
public class WorkScheduleController : ControllerBase
{
    private readonly IWorkScheduleService _workScheduleService;

    public WorkScheduleController(IWorkScheduleService workScheduleService)
    {
        _workScheduleService = workScheduleService;
    }

    /// <summary>
    /// Lấy danh sách lịch làm
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _workScheduleService.GetAllAsync();

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Code = ErrorCode.SUCCESS,
            Message = "Lấy danh sách thành công.",
            Data = data
        });
    }

    /// <summary>
    /// Lấy chi tiết lịch làm
    /// </summary>
    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var data = await _workScheduleService.GetByIdAsync(id);

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Code = ErrorCode.SUCCESS,
            Message = "Lấy dữ liệu thành công.",
            Data = data
        });
    }

    /// <summary>
    /// Tạo lịch làm
    /// </summary>
    [Authorize(Roles = "Admin,Manager")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateWorkScheduleRequest request)
    {
        var data = await _workScheduleService.CreateAsync(request);

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Code = ErrorCode.SUCCESS,
            Message = "Tạo lịch làm thành công.",
            Data = data
        });
    }
    [Authorize(Roles = "Admin,Manager")]
    [HttpPost("import")]
    public async Task<IActionResult> Creates(List<ImportWorkScheduleRequest> requests)
    {
        var userId = int.Parse(
               User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var data = await _workScheduleService.ImportAsync(requests, userId);

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Code = ErrorCode.SUCCESS,
            Message = "Tạo lịch làm thành công.",
            Data = data
        });
    }
    /// <summary>
    /// Cập nhật lịch làm
    /// </summary>
    [Authorize(Roles = "Admin,Manager")]
    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(
        long id,
        [FromBody] UpdateWorkScheduleRequest request)
    {
        var data = await _workScheduleService.UpdateAsync(id, request);

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Code = ErrorCode.SUCCESS,
            Message = "Cập nhật lịch làm thành công.",
            Data = data
        });
    }

    /// <summary>
    /// Xóa lịch làm
    /// </summary>
    [Authorize(Roles = "Admin,Manager")]
    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _workScheduleService.DeleteAsync(id);

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Code = ErrorCode.SUCCESS,
            Message = "Xóa lịch làm thành công."
        });
    }
}