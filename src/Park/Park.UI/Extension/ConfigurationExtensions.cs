using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.Configuration
{
    public static class ConfigurationExtensions
    {
        public static void LoadSettings<T>(this IConfiguration configuration) where T : class, new()
        {
            configuration.GetSection(typeof(T).Name).Get<T>();//反序列化后，Settings的静态属性已经赋值
        }
    }
}
