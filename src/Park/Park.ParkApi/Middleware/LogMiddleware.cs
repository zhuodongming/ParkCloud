using Infrastructure.Helper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.ParkApi.Middleware
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;
        public LogMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            Exception _ex = null;

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _ex = ex;
            }

            stopwatch.Stop();
            await LogData(context, stopwatch.ElapsedMilliseconds, _ex);//记录日志
        }

        //记录日志
        public async Task LogData(HttpContext context, long elapsedMilliseconds, Exception ex)
        {
            var request = context.Request;
            StringBuilder message = new StringBuilder(10 * 1024);
            message.Append($"ExecTime:{elapsedMilliseconds}ms{Environment.NewLine}");
            message.Append($"{request.Method} {request.Scheme}://{request.Host.Value}{request.Path.Value}{request.QueryString.Value}{Environment.NewLine}");
            message.Append($"Authorization:{request.Headers["Authorization"]}{Environment.NewLine}");
            message.Append($"Date:{request.Headers["Date"]}{Environment.NewLine}");
            if (request.Body.CanSeek)
            {
                request.Body.Position = 0;
                message.Append($"ReqBody:{await new StreamReader(request.Body).ReadToEndAsync()}{Environment.NewLine}");
            }

            var response = context.Response;
            message.Append($"StatusCode:{response.StatusCode}{Environment.NewLine}");
            if (response.Body.CanRead)
            {
                response.Body.Position = 0;
                message.Append($"RespBody:{await new StreamReader(response.Body).ReadToEndAsync()}{Environment.NewLine}");
            }

            string log = message.ToString();//日志

            if (ex == null)
            {
                Log.Info(log);
            }
            else
            {
                Log.Error(log, ex);
            }
        }
    }
}
