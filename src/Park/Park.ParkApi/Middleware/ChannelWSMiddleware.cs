using Infrastructure.Helper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Park.ParkApi.Middleware
{
    /// <summary>
    /// WebSocket Channel
    /// </summary>
    public class ChannelWSMiddleware
    {
        private readonly RequestDelegate _next;
        //private WSHandler _wsHandler;

        public ChannelWSMiddleware(RequestDelegate next)//, WSHandler wsHandler
        {
            this._next = next;
            //this._wsHandler = wsHandler;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.WebSockets.IsWebSocketRequest)
            {
                try
                {
                    string parkingID = httpContext.Request.Query["park_id"];
                    string timestamp = httpContext.Request.Query["timestamp"];
                    string sign = httpContext.Request.Query["sign"];
                    WebSocket webSocket = await httpContext.WebSockets.AcceptWebSocketAsync();

                    //await _wsHandler.HandAsync(parkingID, webSocket, timestamp, sign);
                }
                catch (Exception ex)
                {
                    Log.Error("WebSocket connection is error", ex);
                }
            }
            else
            {
                await _next(httpContext);
            }
        }
    }
}
