using Infrastructure.DI;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Helper
{
    public static class HttpContextEx
    {
        public static HttpContext Current
        {
            get
            {
                return IocManager.GetRequiredService<IHttpContextAccessor>().HttpContext;
            }
        }
    }
}
