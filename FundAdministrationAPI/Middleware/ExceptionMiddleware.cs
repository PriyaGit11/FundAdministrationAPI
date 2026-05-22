using Microsoft.AspNetCore.Mvc; 
using System.Text.Json; 
 
namespace FundAdministrationAPI.Middleware 
{ 
    public class ExceptionMiddleware 
    { 
        private readonly RequestDelegate _next; 
        private readonly ILogger<ExceptionMiddleware> _logger;
 
        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
 
        public async Task InvokeAsync(HttpContext context) 
        { 
            try 
            { 
                await _next(context); 
            } 
            catch (Exception ex) 
            { 
                _logger.LogError(ex, ex.Message);

                context.Response.ContentType = "application/json";

                context.Response.StatusCode =
                    StatusCodes.Status500InternalServerError;

                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Internal Server Error",
                    Detail = ex.Message,
                    Instance = context.Request.Path
                };

                var response = JsonSerializer.Serialize(problemDetails);

                await context.Response.WriteAsync(response);
            } 
        } 
    } 
}