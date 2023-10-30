using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Rectangle.API.Middleware
{
    public class ErrorHandlerMiddleware
    {
        
        private readonly RequestDelegate next;
        private readonly ILogger<ErrorHandlerMiddleware> log;

        public ErrorHandlerMiddleware(RequestDelegate next,  ILogger<ErrorHandlerMiddleware> log)
        {
            this.next = next;
            this.log = log;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next.Invoke(context);
            }
            catch (Exception error)
            {
                log.LogError(error, "Request failed");
                await HandleExceptionAsync(context, error);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(
                JsonSerializer.Serialize(
                new ErrorDetails()
                {
                    StatusCode = context.Response.StatusCode,
                    Message = exception.Message
                }));
        }
    }

    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }

}