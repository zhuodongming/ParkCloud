using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Infrastructure.Helper
{
    /// <summary>
    /// 压缩解压 Helper
    /// </summary>
    public sealed class Compress
    {
        /// <summary>
        /// GZip压缩
        /// </summary>
        /// <param name="sourceStream"></param>
        /// <returns></returns>
        public static MemoryStream GZip(Stream sourceStream)
        {
            try
            {
                MemoryStream responseStream = new MemoryStream();
                using (GZipStream gzipStream = new GZipStream(responseStream, CompressionMode.Compress))
                {
                    sourceStream.CopyTo(gzipStream);
                    responseStream.Position = 0;
                    return responseStream;
                }
            }
            catch
            {
                Log.Error("GZip压缩出错");
                throw;
            }
        }

        /// <summary>
        /// GZip解压缩
        /// </summary>
        /// <param name="sourceStream"></param>
        /// <returns></returns>
        public static MemoryStream UnGZip(Stream sourceStream)
        {
            try
            {
                using (GZipStream gzipStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                {
                    MemoryStream responseStream = new MemoryStream();
                    gzipStream.CopyTo(responseStream);
                    responseStream.Position = 0;
                    return responseStream;
                }
            }
            catch
            {
                Log.Error("GZip解压缩出错");
                throw;
            }
        }

        /// <summary>
        /// Zip压缩
        /// </summary>
        /// <param name="sourceStream"></param>
        /// <returns></returns>
        public static MemoryStream Zip(Stream sourceStream)
        {
            try
            {
                MemoryStream responseStream = new MemoryStream();
                using (ZipArchive archive = new ZipArchive(responseStream, ZipArchiveMode.Update))
                {
                    ZipArchiveEntry entry = archive.CreateEntry("file");
                    using (Stream zipStream = entry.Open())
                    {
                        sourceStream.CopyTo(zipStream);
                        responseStream.Position = 0;
                        return responseStream;
                    }
                }
            }
            catch
            {
                Log.Error("Zip压缩出错");
                throw;
            }
        }


        /// <summary>
        /// Zip解压缩
        /// </summary>
        /// <param name="sourceStream"></param>
        /// <returns></returns>
        public static MemoryStream UnZip(Stream sourceStream)
        {
            try
            {
                using (ZipArchive archive = new ZipArchive(sourceStream, ZipArchiveMode.Read))
                {
                    ZipArchiveEntry entry = archive.Entries[0];
                    using (Stream zipStream = entry.Open())
                    {
                        MemoryStream responseStream = new MemoryStream();
                        zipStream.CopyTo(responseStream);
                        responseStream.Position = 0;
                        return responseStream;
                    }
                }
            }
            catch
            {
                Log.Error("Zip解压缩出错");
                throw;
            }
        }
    }
}
