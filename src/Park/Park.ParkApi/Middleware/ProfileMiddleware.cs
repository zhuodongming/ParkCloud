using Infrastructure.Helper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Park.ParkApi.Middleware
{
    /// <summary>
    /// 性能中间件
    /// </summary>
    public class ProfileMiddleware
    {
        private readonly RequestDelegate _next;

        public ProfileMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            await _next(httpContext);

            //record of more than 100 milliseconds request
            stopwatch.Stop();
            if (stopwatch.ElapsedMilliseconds > 100)
            {
                string message = $"RouteData:{Json.ToJson(httpContext.Request.Path.ToString())} execute time greater than 100 milliseconds";
                Log.Warn(message);
            }
        }
    }
}
