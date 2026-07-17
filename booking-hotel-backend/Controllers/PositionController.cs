using booking_hotel_backend.Common;
using booking_hotel_backend.Models.DTOs.Position;
using booking_hotel_backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace booking_hotel_backend.Controllers
{
    [ApiController]
    [Route("api/positions")]
    public class PositionController : ControllerBase
    {
        private readonly IPositionService _positionService;

        public PositionController(IPositionService positionService)
        {
            _positionService = positionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _positionService.GetAllAsync();

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Code = ErrorCode.SUCCESS,
                Message = "Lấy danh sách chức vụ thành công.",
                Data = data
            });
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            var data = await _positionService.GetByIdAsync(id);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Code = ErrorCode.SUCCESS,
                Message = "Lấy chức vụ thành công.",
                Data = data
            });
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreatePositionRequest request)
        {
            var data = await _positionService.CreateAsync(request);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Code = ErrorCode.SUCCESS,
                Message = "Tạo chức vụ thành công.",
                Data = data
            });
        }

        //[Authorize(Roles = "Admin")]
        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, UpdatePositionRequest request)
        {
            var data = await _positionService.UpdateAsync(id, request);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Code = ErrorCode.SUCCESS,
                Message = "Cập nhật chức vụ thành công.",
                Data = data
            });
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _positionService.DeleteAsync(id);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Code = ErrorCode.SUCCESS,
                Message = "Xóa chức vụ thành công."
            });
        }
    }
}