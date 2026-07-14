using System.Text.Json;
using booking_hotel_backend.Common;
using booking_hotel_backend.Common.Exceptions;

namespace booking_hotel_backend.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ApiException ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = ex.StatusCode;

            var response = new ApiResponse<object>
            {
                Success = false,
                Code = ex.ErrorCode,
                Message = ex.Message,
                Data = null
            };

            await context.Response.WriteAsync(
    JsonSerializer.Serialize(response, new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    }));
        }
        catch (Exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;

            var response = new ApiResponse<object>
            {
                Success = false,
                Code = ErrorCode.SERVER_001,
                Message = "Internal Server Error"
            };

            await context.Response.WriteAsync(
    JsonSerializer.Serialize(response, new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    }));
        }
    }
}