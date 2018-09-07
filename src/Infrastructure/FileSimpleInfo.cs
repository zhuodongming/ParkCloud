using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    /// <summary>
    /// 文件简单信息
    /// </summary>
    public class FileSimpleInfo
    {
        public string FileName { get; set; }//文件名
        public byte[] Content { get; set; }//内容
    }
}
