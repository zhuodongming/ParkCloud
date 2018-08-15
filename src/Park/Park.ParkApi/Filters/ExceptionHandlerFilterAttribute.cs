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
            message.Append("Unhandled exception:" + Environment.NewLine);
            message.Append("Method: " + request.Method + Environment.NewLine);
            message.Append("URI: " + request.Scheme + "://" + request.Host.Value + request.Path.Value + request.QueryString.Value + Environment.NewLine);
            message.Append("Headers: " + Json.ToJson(request.Headers) + Environment.NewLine);
            message.Append("RouteData: " + Json.ToJson(context.RouteData.Values) + Environment.NewLine);

            using (StreamReader sr = new StreamReader(request.Body))
            {
                message.Append("Body: " + await sr.ReadToEndAsync());
            }

            Log.Error(message.ToString(), context.Exception);

            await base.OnExceptionAsync(context);
        }
    }
}
