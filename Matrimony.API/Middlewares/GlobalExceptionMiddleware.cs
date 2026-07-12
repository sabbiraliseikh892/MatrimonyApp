using Matrimony.Shared.Exceptions;
using Matrimony.Shared.Responses;
using System;
using System.Text.Json;

namespace Matrimony.API.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        public GlobalExceptionMiddleware(RequestDelegate next,
    ILogger<GlobalExceptionMiddleware> logger)
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
                _logger.LogError(
    ex,
    "Unhandled exception for User {User}",
    context.User?.Identity?.Name);
                await HandleExceptionAsync(context, ex);
            }
        }
        private static async Task HandleExceptionAsync(
           HttpContext context,
           Exception exception)
        {
            context.Response.ContentType = "application/json";

            var statusCode = StatusCodes.Status500InternalServerError;

            switch (exception)
            {
                case BusinessException:
                case ValidationException:
                    statusCode = StatusCodes.Status400BadRequest;
                    break;

                case NotFoundException:
                    statusCode = StatusCodes.Status404NotFound;
                    break;

                case UnauthorizedException:
                    statusCode = StatusCodes.Status401Unauthorized;
                    break;
            }

            context.Response.StatusCode = statusCode;

            //switch (exception)
            //{
            //    case BusinessException:
            //    case ValidationException:
            //        context.Response.StatusCode = StatusCodes.Status400BadRequest;
            //        break;

            //    case NotFoundException:
            //        context.Response.StatusCode = StatusCodes.Status404NotFound;
            //        break;

            //    case UnauthorizedException:
            //        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //        break;

            //    default:
            //        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            //        response.Message = "An unexpected error occurred.";
            //        break;
            //}

            var response = ApiResponse<object>.FailureResponse(
    statusCode == StatusCodes.Status500InternalServerError
        ? "An unexpected error occurred."
        : exception.Message,
    statusCode);
            var json = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(json);
        }

    }
}
