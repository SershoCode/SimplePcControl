using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SimplePcControl.Host.Models;

namespace SimplePcControl.Host.Middlewares
{
    public class CheckHeaderTokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly MiddlewareOptions _options;

        public CheckHeaderTokenMiddleware(RequestDelegate next, IOptions<MiddlewareOptions> options)
        {
            _next = next;
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (httpContext.Request.Headers["Bearer"] == _options.BearerToken)
            {
                await _next(httpContext);
            }
            else
            {
                httpContext.Response.StatusCode = 200;
                httpContext.Response.ContentType = "application/json";

                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(new ApiResponse(false, "Неверный токен запроса")));
            }
        }
    }
}
