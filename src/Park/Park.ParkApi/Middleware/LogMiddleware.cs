using Infrastructure.Helper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.ParkApi.Middleware
{
    /// <summary>
    /// 日志中间件
    /// </summary>
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;

        public LogMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            await _next(httpContext);

            await LogRequest(httpContext);//记录日志
        }

        //记录日志
        public async Task LogRequest(HttpContext httpContext)
        {
            var request = httpContext.Request;
            StringBuilder message = new StringBuilder(4 * 1024);
            message.Append("Method: " + request.Method + Environment.NewLine);
            message.Append("URI: " + request.Scheme + "://" + request.Host.Value + request.Path.Value + request.QueryString.Value + Environment.NewLine);
            message.Append("Headers: " + request.Headers.ToJson() + Environment.NewLine);
            message.Append("RouteData: " + httpContext.Request.Path.ToJson() + Environment.NewLine);

            using (StreamReader sr = new StreamReader(request.Body))
            {
                message.Append("Body: " + await sr.ReadToEndAsync());
            }

            Log.Info(message.ToString());
        }
    }
}
