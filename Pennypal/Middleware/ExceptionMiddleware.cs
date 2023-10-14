using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Pennypal.Middleware;

public class ExceptionMiddleware
{
    private readonly IHostEnvironment _env;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly RequestDelegate _next;
    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, IHostEnvironment env, RequestDelegate next)
    {
        _logger = logger;
        _env = env;
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = 500;

            var response = new ProblemDetails
            {
                Status = 500,
                Title = ex.Message,
                Detail = _env.IsDevelopment() ? ex.StackTrace : null
            };   

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(response, options);

            await httpContext.Response.WriteAsync(json);    
        }
    }
}