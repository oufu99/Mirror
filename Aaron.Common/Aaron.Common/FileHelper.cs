using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aaron.Common
{
    public class FileHelper
    {
        static string ZhiXuanFlag = "校对版全本";
        static List<string> PictureSffix = new List<string> { "bmp", "jpg", "png", "tif", "gif", "psd" };
        static List<string> AduioSffix = new List<string> { "wave", "mpeg", "mp3", "m4a" };
        static List<string> TextSffix = new List<string> { "txt", "pdf", "doc", };

        /// <summary>
        /// 传入完整路径,和新文件名就可以了
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="newFileName"></param>
        public static void ReNameFile(string fullPath, string newFileName)
        {
            //传入Url,重命名   传入一个正则 一个文件根目录 然后替换里面所有的txt文件名
            FileInfo fInfo = new FileInfo(fullPath);

            //回去最后一个/
            var index = fullPath.LastIndexOf(@"\") + 1;
            if (index <= 0)
            {
                return;
            }
            var lastIndex = fullPath.LastIndexOf(@".");
            var oldFileName = fullPath.Substring(index, lastIndex - index);
            newFileName = fullPath.Replace(oldFileName, newFileName);
            fInfo.MoveTo(Path.Combine(newFileName));
        }

        /// <summary>
        /// 获取目录下所有的文件名形成 List 
        /// </summary>
        /// <param name="dirs"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<string> GetDirectorAllFile(string dirs, List<string> list)
        {
            //绑定到指定的文件夹目录
            DirectoryInfo dir = new DirectoryInfo(dirs);
            //检索表示当前目录的文件和子目录
            FileSystemInfo[] fsinfos = dir.GetFileSystemInfos();
            //遍历检索的文件和子目录
            foreach (FileSystemInfo fileInfo in fsinfos)
            {
                //判断是否为空文件夹　　
                if (fileInfo is DirectoryInfo)
                {
                    //递归调用
                    GetDirectorAllFile(fileInfo.FullName, list);
                }
                else
                {
                    Console.WriteLine(fileInfo.FullName);
                    //将得到的文件名称全路径放入到集合中
                    list.Add(fileInfo.FullName);
                }
            }
            return list;
        }

        /// <summary>
        /// 文件移动  传入全路径
        /// </summary>
        /// <param name="sourcPath"></param>
        /// <param name="targetPath"></param>
        /// <param name="isForce"></param>
        public static void MoveFile(string sourcPath, string targetPath, bool isForce = false)
        {
            //如果是强制覆盖
            if (isForce)
            {
                if (File.Exists(targetPath))
                {
                    File.Delete(targetPath);
                }
                File.Move(sourcPath, targetPath);
            }
            else
            {
                if (!File.Exists(targetPath))
                {
                    File.Move(sourcPath, targetPath);
                }
            }
        }
      
        /// <summary>
        /// 获取全路径的后缀名
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetFileSffix(string fileName)
        {
            var lastIndex = fileName.LastIndexOf(@".") + 1;
            if (lastIndex <= 0)
            {
                return "";
            }
            return fileName.Substring(lastIndex, fileName.Length - lastIndex);
        } 

      }
}
