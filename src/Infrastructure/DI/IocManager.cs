using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infrastructure.DI
{
    /// <summary>
    /// Container manager
    /// </summary>
    public sealed class IocManager
    {
        private static ServiceProvider provider;

        //Ioc容器初始化
        public static void Init(IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            provider = services.BuildServiceProvider();
        }

        public static T GetRequiredService<T>() where T : class
        {
            if (provider == null)
            {
                throw new Exception("IocManager is not Init");
            }
            return provider.GetRequiredService<T>();
        }
    }
}
