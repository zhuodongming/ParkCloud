using Infrastructure.Helper;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.ParkApi.Filters
{
    public class ExceptionHandlerFilterAttribute : ExceptionFilterAttribute
    {
        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            var request = context.HttpContext.Request;
            StringBuilder message = new StringBuilder(4 * 1024);
            message.Append($"{request.Method} {request.Scheme}://{request.Host.Value}{request.Path.Value}{request.QueryString.Value}{Environment.NewLine}");
            message.Append($"Authorization:{request.Headers["Authorization"]}{Environment.NewLine}");
            message.Append($"Date:{request.Headers["Date"]}{Environment.NewLine}");
            if (request.Body.CanSeek)
            {
                request.Body.Position = 0;
                message.Append($"ReqBody:{await new StreamReader(request.Body).ReadToEndAsync()}");
            }

            Log.Error(message.ToString(), context.Exception);

            await base.OnExceptionAsync(context);
        }
    }
}
