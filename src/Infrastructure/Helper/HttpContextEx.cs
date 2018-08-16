using Infrastructure.DI;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Helper
{
    public class HttpContextEx
    {
        public static HttpContext Current
        {
            get
            {
                var factory = IocManager.GetRequiredService<IHttpContextAccessor>();
                return factory.HttpContext;
            }
        }
    }
}
