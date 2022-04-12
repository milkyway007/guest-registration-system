using Application.Core;
using FluentValidation;
using System.Net;
using System.Text.Json;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger,
            IHostEnvironment env)
        {
            _env = env;
            _logger = logger;
            _next = next;
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

                string json;
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                context.Response.ContentType = Constants.CONTENT_TYPE;
                if (ex.GetType() == typeof(ValidationException))
                {
                    json = JsonSerializer.Serialize(((ValidationException)ex).Errors, options);                    
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                    var response = _env.IsDevelopment()
                        ? new AppException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                        : new AppException(context.Response.StatusCode, Constants.SERVER_ERROR);
                    
                    json = JsonSerializer.Serialize(response, options);
                }

                await context.Response.WriteAsync(json);
            }
        }
    }
}