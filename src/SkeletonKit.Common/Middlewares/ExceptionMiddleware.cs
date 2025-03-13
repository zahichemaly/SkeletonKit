using CME.Common.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CME.Common.Middlewares
{
    internal class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger(nameof(ExceptionMiddleware));
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "An error occurred while processing your request");
                await HandleExceptionAsync(httpContext, exception);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            ErrorDetails errorDetails = new ErrorDetails();
            errorDetails.Message = exception.Message;

            if (exception is ApiException apiException)
            {
                context.Response.StatusCode = apiException.StatusCode;
                errorDetails.Code = apiException.ResponseCode;
            }
            else if (exception is ValidationErrorException validationException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                ValidationErrorDetails validationErrorDetails = new ValidationErrorDetails()
                {
                    Message = "One or more validation errors has occurred",
                    Code = Constants.ErrorCode.ValidationError,
                };

                if (validationException.Errors is not null)
                {
                    validationErrorDetails.Errors = validationException.Errors;
                }

                return context.Response.WriteAsync(validationErrorDetails.ToString());
            }
            else
            {
                if (context.Response.StatusCode != (int)HttpStatusCode.Unauthorized)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                }
                errorDetails.Code = Constants.ErrorCode.GenericError;
            }

            return context.Response.WriteAsync(errorDetails.ToString());
        }
    }

    public static class ExceptionMiddlewareExtensions
    {
        public static void UseExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
