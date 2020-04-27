using FastDFS.Client;
using FastDFS.Client.Common;
using FastDFS.Client.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Coldairarrow.Util.Helper
{
    public class FastDFSHelper
    {
        private static List<IPEndPoint> trackerIPs = new List<IPEndPoint>();
        private static IPEndPoint endPoint;
        private static StorageNode storageNode;
        private static string groupName = ConfigHelper.GetValue("FastDFS", "fastdfs_groupname");

        /// <summary>
        /// 链接 FASTDFS
        /// </summary>
        static FastDFSHelper()
        {
            string[] trackers = ConfigHelper.GetValue("FastDFS", "fastdfs_trackers").Split(new char[','], StringSplitOptions.RemoveEmptyEntries);
            string[] storages = ConfigHelper.GetValue("FastDFS", "fastdfs_storages").Split(new char[','], StringSplitOptions.RemoveEmptyEntries);
            int port = int.Parse(ConfigHelper.GetValue("FastDFS", "fastdfs_port"));

            foreach (var onetracker in trackers)
            {
                trackerIPs.Add(new IPEndPoint(IPAddress.Parse(onetracker), port));
            }
            ConnectionManager.Initialize(trackerIPs);
            storageNode = FastDFSClient.GetStorageNode(groupName);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="fileStream">文件流</param>
        /// <param name="fileExt">文件后缀</param>
        /// <returns>返回主文件路径</returns>
        public static string UploadFile(string fileBase64, string fileName)
        {
            byte[] bytes = fileBase64.ToBytes_FromBase64Str();
            //Stream stream = new MemoryStream(bytes);
            //using (BinaryReader reader = new BinaryReader(stream))
            //{
            //    bytes = reader.ReadBytes((int)stream.Length);
            //}

            //主文件
            return FastDFSClient.UploadFile(storageNode, bytes, Path.GetExtension(fileName).Replace(".", ""));
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="fileStream">文件流</param>
        /// <param name="fileExt">文件后缀</param>
        /// <returns>返回主文件路径</returns>
        public static string UploadFile(Stream fileStream, string fileExt)
        {
            byte[] content = new byte[fileStream.Length];
            using (BinaryReader reader = new BinaryReader(fileStream))
            {
                content = reader.ReadBytes((int)fileStream.Length);
            }

            //主文件
            string fileName = FastDFSClient.UploadFile(storageNode, content, fileExt);
            return fileName;
        }
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <param name=""></param>
        /// <returns>文件流</returns>
        public FileStream DownloadFile(string fileName)
        {

            FDFSFileInfo fileInfo = FastDFSClient.GetFileInfo(storageNode, fileName);

            FileStream fileStream = new FileStream(Path.GetTempFileName(), FileMode.Create);
            if (fileInfo.FileSize >= 1024)//如果文件大小大于1KB  分次写入
            {
                long offset = 0;
                long len = 1024;
                while (len > 0)
                {
                    byte[] buffer = new byte[len];
                    buffer = FastDFSClient.DownloadFile(storageNode, fileName, offset, len);
                    fileStream.Write(buffer, 0, int.Parse(len.ToString()));
                    fileStream.Flush();
                    offset = offset + len;
                    len = (fileInfo.FileSize - offset) >= 1024 ? 1024 : (fileInfo.FileSize - offset);
                }
                return fileStream;
            }
            else//如果文件大小小小于1KB  直接写入文件
            {
                byte[] buffer = new byte[fileInfo.FileSize];
                buffer = FastDFSClient.DownloadFile(storageNode, fileName);
                //FileStream fileStream = new FileStream(targetPath, FileMode.OpenOrCreate, FileAccess.Write);
                fileStream.Write(buffer, 0, buffer.Length);
                fileStream.Flush();
                fileStream.Close();
                return fileStream;
            }
        }


        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <param name=""></param>
        /// <returns>文件流</returns>
        public byte[] DownloadFileByte(string fileName)
        {
            FDFSFileInfo fileInfo = FastDFSClient.GetFileInfo(storageNode, fileName);
            byte[] buffer = new byte[fileInfo.FileSize];
            buffer = FastDFSClient.DownloadFile(storageNode, fileName);

            FileStream fs = new FileStream("D://down123.png", FileMode.Create, FileAccess.Write);
            fs.Write(buffer, 0, buffer.Length);
            fs.Flush();
            fs.Close();

            return buffer;
        }

        /// <summary>
        /// 下载文件 流模式
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <param name=""></param>
        /// <returns>文件流</returns>
        public Stream DownloadFileStream(string fileName)
        {
            FDFSFileInfo fileInfo = FastDFSClient.GetFileInfo(storageNode, fileName);

            Stream stream = new MemoryStream();


            if (fileInfo.FileSize >= 1024)//如果文件大小大于1KB  分次写入
            {
                long offset = 0;
                long len = 1024;
                while (len > 0)
                {
                    byte[] buffer = new byte[len];
                    buffer = FastDFSClient.DownloadFile(storageNode, fileName, offset, len);
                    stream.Write(buffer, 0, int.Parse(len.ToString()));
                    stream.Flush();
                    offset = offset + len;
                    len = (fileInfo.FileSize - offset) >= 1024 ? 1024 : (fileInfo.FileSize - offset);
                }

            }
            else//如果文件大小小小于1KB  直接写入文件
            {
                byte[] buffer = new byte[fileInfo.FileSize];
                buffer = FastDFSClient.DownloadFile(storageNode, fileName);
                //FileStream fileStream = new FileStream(targetPath, FileMode.OpenOrCreate, FileAccess.Write);
                stream.Write(buffer, 0, buffer.Length);
                stream.Flush();
            }
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        /// <summary>
        /// 根据文件名称获取文件浏览路径
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetFilePath(string fileName)
        {
            string fastdfs_downloadServer = ConfigHelper.GetValue("FastDFS", "fastdfs_downloadServer");
            string groupname = ConfigHelper.GetValue("FastDFS", "fastdfs_groupname");
            return fastdfs_downloadServer + "/" + groupname + "/" + fileName;
        }
        /// <summary>
        /// 获取服务器路径
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetServicePath()
        {
            string fastdfs_downloadServer = ConfigHelper.GetValue("FastDFS", "fastdfs_downloadServer");
            string groupname = ConfigHelper.GetValue("FastDFS", "fastdfs_groupname");
            return fastdfs_downloadServer + "/" + groupname + "/";
        }
    }
}
