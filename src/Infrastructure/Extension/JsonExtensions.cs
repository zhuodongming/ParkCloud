using Infrastructure.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    /// <summary>
    /// Json 扩展方法
    /// </summary>
    public static class JsonExtensions
    {
        /// <summary>
        /// 转换为json字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// 转换为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T ToObject<T>(this string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch
            {
                Log.Error($"解析Json出错:{json}");
                throw;
            }
        }
    }
}
