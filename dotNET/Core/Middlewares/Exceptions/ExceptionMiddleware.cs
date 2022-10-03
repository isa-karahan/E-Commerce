using Core.Utilities.Results;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Core.Middlewares.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
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
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            if (exception is ValidationException)
                return CreateValidationException(context, exception);

            return context.Response.WriteAsync(
                Result.Error(exception.Message).ToString()
                );
        }

        private Task CreateValidationException(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);
            var errors = ((ValidationException)exception).Errors;

            string message = "";

            foreach (var error in errors)
            {
                message += error.ErrorMessage.ToString() + "\n";
            }

            return context.Response.WriteAsync(
                Result.Error(message).ToString()
                );
        }
    }
}
