using booking_hotel_backend.Common;
using booking_hotel_backend.Common.Exceptions;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace booking_hotel_backend.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly JsonSerializerOptions _jsonOptions;

    public ExceptionMiddleware(
        RequestDelegate next,
        IOptions<JsonOptions> jsonOptions)
    {
        _next = next;
        _jsonOptions = jsonOptions.Value.JsonSerializerOptions;
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
    JsonSerializer.Serialize(response, _jsonOptions));
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
    JsonSerializer.Serialize(response, _jsonOptions));
        }
    }
}