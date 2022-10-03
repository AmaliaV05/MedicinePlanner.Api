using MedicinePlanner.Api.Extensions;
using MedicinePlanner.BusinessLogic.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MedicinePlanner.Api.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (IdNotFoundException infe)
            {
                await HandleExceptionAsync(httpContext, infe);
            }
            catch (MedicineNotFoundException mnfe)
            {
                await HandleExceptionAsync(httpContext, mnfe);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var message = exception switch
            {
                IdNotFoundException => exception.Message,
                MedicineNotFoundException => exception.Message,
                _ => exception.Message
            };
            var stack = exception switch
            {
                IdNotFoundException => exception.StackTrace,
                MedicineNotFoundException => exception.StackTrace,
                _ => exception.StackTrace
            };
            await httpContext.Response.AddErrorMessage(httpContext.Response.StatusCode, message, stack);
        }
    }
}
