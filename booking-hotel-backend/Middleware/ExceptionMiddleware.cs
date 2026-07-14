using booking_hotel_backend.Common;
using booking_hotel_backend.Common.Exceptions;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
namespace booking_hotel_backend.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly IWebHostEnvironment _environment;
    private readonly ILogger<ExceptionMiddleware> _logger;
    public ExceptionMiddleware(
      RequestDelegate next,
      IOptions<JsonOptions> jsonOptions,
      IWebHostEnvironment environment,
      ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _jsonOptions = jsonOptions.Value.JsonSerializerOptions;
        _environment = environment;
        _logger = logger;
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
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            var response = new ApiResponse<object>
            {
                Success = false,
                Code = ErrorCode.SERVER_001,
                Message = _environment.IsDevelopment()
                    ? ex.Message
                    : "Internal Server Error",
                Data = null,
                Errors = _environment.IsDevelopment()
                    ? ex.StackTrace
                    : null
            };

            await context.Response.WriteAsync(
    JsonSerializer.Serialize(response, _jsonOptions));
        }
    }
}