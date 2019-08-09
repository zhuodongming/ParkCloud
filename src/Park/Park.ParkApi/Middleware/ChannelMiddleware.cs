using Infrastructure.Helper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Park.ParkApi.Middleware
{
    public class ChannelMiddleware
    {
        private readonly RequestDelegate _next;
        public ChannelMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                if (context.Request.Path == "/channel.pkc")
                {
                    string parkingID = context.Request.Query["parkingid"];
                    string timestamp = context.Request.Query["timestamp"];
                    string sign = context.Request.Query["sign"];
                    using (WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync())
                    {
                        //await new ChannelHandler().HandAsync(webSocket, parkingID, timestamp, sign);
                    }
                }
                else
                {
                    context.Response.StatusCode = 404;
                }
            }
            else
            {
                await _next(context);
            }
        }
    }
}
