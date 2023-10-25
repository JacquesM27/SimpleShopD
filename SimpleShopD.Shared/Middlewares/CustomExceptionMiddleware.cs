using Microsoft.AspNetCore.Http;
using SimpleShopD.Shared.Abstractions.Exceptions;
using System.Net;
using System.Text.Json;

namespace SimpleShopD.Shared.Middlewares
{
    internal sealed class CustomExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            } 
            catch (CustomException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.Headers.Add("content-type", "application/json");

                var errorCode = ex.GetType().Name.Replace("Exception", string.Empty);
                var json = JsonSerializer.Serialize(new { ErrorCode = errorCode, ex.Message });
                await context.Response.WriteAsync(json);
            }
        }
    }
}
