using Infrastructure.DI;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Infrastructure.Helper
{
    /// <summary>
    /// Log Helper
    /// </summary>
    public sealed class Log
    {
        //private static ILog log = null;
        //static Log()
        //{
        //    GlobalContext.Properties["BaseDirectory"] = AppContext.BaseDirectory;//设置全局属性
        //    ILoggerRepository repository = LogManager.CreateRepository(Guid.NewGuid().ToString());
        //    FileInfo fileInfo = new FileInfo(AppContext.BaseDirectory + "log4net.config");//读取配置文件
        //    XmlConfigurator.ConfigureAndWatch(repository, fileInfo);
        //    log = LogManager.GetLogger(repository.Name, "DefaultLogger");
        //}

        //public static void Debug(object message, Exception exception = null)
        //{
        //    log.Debug(message, exception);
        //}

        //public static void Info(object message, Exception exception = null)
        //{
        //    log.Info(message, exception);
        //}

        //public static void Warn(object message, Exception exception = null)
        //{
        //    log.Warn(message, exception);
        //}

        //public static void Error(object message, Exception exception = null)
        //{
        //    log.Error(message, exception);
        //}

        private static ILogger logger = IocManager.GetRequiredService<ILogger<Log>>();

        public static void Debug(string message, Exception exception = null)
        {
            if (logger.IsEnabled(LogLevel.Debug))
            {
                Console.WriteLine($"{DateTimeOffset.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff")} DEBUG [{AppDomain.CurrentDomain.FriendlyName}] [{new StackFrame(1).GetMethod().ReflectedType.FullName}]-{message}");
                if (exception != null)
                {
                    Console.WriteLine(exception.ToString());
                }
            }
        }

        public static void Info(string message, Exception exception = null)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                Console.WriteLine($"{DateTimeOffset.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff")} INFO [{AppDomain.CurrentDomain.FriendlyName}] [{new StackFrame(1).GetMethod().ReflectedType.FullName}]-{message}");
                if (exception != null)
                {
                    Console.WriteLine(exception.ToString());
                }
            }
        }

        public static void Warn(string message, Exception exception = null)
        {
            Console.WriteLine($"{DateTimeOffset.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff")} WARN [{AppDomain.CurrentDomain.FriendlyName}] [{new StackFrame(1).GetMethod().ReflectedType.FullName}]-{message}");
            if (exception != null)
            {
                Console.WriteLine(exception.ToString());
            }
        }

        public static void Error(string message, Exception exception = null)
        {
            Console.WriteLine($"{DateTimeOffset.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff")} ERROR [{AppDomain.CurrentDomain.FriendlyName}] [{new StackFrame(1).GetMethod().ReflectedType.FullName}]-{message}");
            if (exception != null)
            {
                Console.WriteLine(exception.ToString());
            }
        }
    }
}
