using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Park.UI.Middleware
{
    public class HttpContextBodyBufferingMiddleware
    {
        private readonly RequestDelegate _next;
        public HttpContextBodyBufferingMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            context.Request.EnableBuffering();

            using (var ms = new MemoryStream())
            {
                var originalResponseStream = context.Response.Body;
                context.Response.Body = ms;

                await _next(context);

                ms.Position = 0;
                await ms.CopyToAsync(originalResponseStream);
                context.Response.Body = originalResponseStream;
            }
        }
    }
}
