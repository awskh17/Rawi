using StoryApp.Exceptions;
using StoryApp.Exceptions.Abstraction;
using System.Text.Json;

namespace StoryApp.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {

            try
            {
                await _next(context);
            }
            catch (Exception ex) when (ex is IProblemDetailsProvider provider)
            {
                await ServerError(context, provider);
            }
            catch (Exception ex)
            {
                await ServerError(context, ex.Message);
            }
        }

        private async Task ServerError(HttpContext context, IProblemDetailsProvider provider)
        {
            context.Response.StatusCode = provider switch
            {
                FailedPreconditionException => StatusCodes.Status412PreconditionFailed,
                AlreadyExistsException => StatusCodes.Status409Conflict,
                NotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                InvalidArguementException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status400BadRequest
            };
            context.Response.ContentType = "application/json";
            var errorData = JsonSerializer.Serialize(provider.GetProblemDetails());
            await context.Response.WriteAsync(errorData);
        }

        private async Task ServerError(HttpContext context, string message)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(new ServiceProblemDetails
            {
                Type = "Server error",
                Title = message,
            }));
        }
    }
}

