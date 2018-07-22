using Microsoft.Extensions.Logging;
using Infrastructure.DI;
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
