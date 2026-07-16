using booking_hotel_backend.Common;
using booking_hotel_backend.Services.Interfaces;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace booking_hotel_backend.Controllers
{
    [ApiController]
    [Route("api/upload")]
    public class UploadController : ControllerBase
    {
        private readonly ICloudinaryService _cloudinaryService;

        public UploadController(ICloudinaryService cloudinaryService)
        {
            _cloudinaryService = cloudinaryService;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var url = await _cloudinaryService.UploadImageAsync(file);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Code = ErrorCode.SUCCESS,
                Message = "Upload thành công.",
                Data =url

            });
            
        }
        [HttpPost("multiple")]
        public async Task<IActionResult> UploadMultiple(List<IFormFile> files)
        {
            var urls = await _cloudinaryService.UploadImagesAsync(files);
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Code = ErrorCode.SUCCESS,
                Message = "Upload thành công.",
                Data = urls
            });
            
        }
    }
}
