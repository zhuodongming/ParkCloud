using FluentFTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Client
{
    /// <summary>
    /// Ftp Client
    /// </summary>
    public class Ftp : FtpClient
    {
        public Ftp(string host, int port, string user, string pass) : base(host, port, user, pass)
        {

        }

        //获取文件名列表
        public async Task<string[]> GetFileNameListAsync()
        {
            var itemList = await GetListingAsync();
            string[] fileNameList = itemList.Where(i => i.Type == FtpFileSystemObjectType.File).Select(i => i.Name).ToArray();
            return fileNameList;
        }
    }
}
