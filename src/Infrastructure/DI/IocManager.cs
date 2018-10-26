using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

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

            //所有程序集 和程序集下类型
            List<TypeInfo> allTypes = new List<TypeInfo>();
            List<CompilationLibrary> libs = DependencyContext.Default.CompileLibraries.Where(lib => lib.Type == "project").ToList();//排除所有的系统程序集、Nuget下载包
            libs.ForEach(lib =>
            {
                Assembly assembly = Assembly.Load(new AssemblyName(lib.Name));
                allTypes.AddRange(assembly.DefinedTypes);
            });

            //注册TransientDependencyAttribute
            var transientTypes = allTypes.Where(t => t.GetCustomAttribute<TransientAttribute>() != null);
            BathAddService(transientTypes, allTypes, services.AddTransient);

            //注册ScopedDependencyAtrribute
            var scopedTypes = allTypes.Where(t => t.GetCustomAttribute<ScopedAttribute>() != null);
            BathAddService(scopedTypes, allTypes, services.AddScoped);

            //注册SingletonDependencyAttribute
            var singletonTypes = allTypes.Where(t => t.GetCustomAttribute<SingletonAttribute>() != null);
            BathAddService(singletonTypes, allTypes, services.AddSingleton);

            provider = services.BuildServiceProvider();
        }

        //批量注入服务
        private static void BathAddService(IEnumerable<TypeInfo> types, List<TypeInfo> allTypes, Func<Type, Type, IServiceCollection> AddService)
        {
            foreach (TypeInfo type in types)
            {
                AddService(type, type);//注册自身

                //Type baseType = null;
                //if (type.BaseType != typeof(Object))
                //{
                //    baseType = type.BaseType;
                //}
                //else if (type.ImplementedInterfaces.Count() > 0)
                //{
                //    baseType = type.ImplementedInterfaces.First();
                //}
                //else
                //{
                //    continue;
                //}

                //baseType = allTypes.FirstOrDefault(t => t.Name == baseType.Name && t.Namespace == baseType.Namespace);
                //if (baseType != null)
                //{
                //    AddService(baseType, type);//注册接口、基类
                //}
            }
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
