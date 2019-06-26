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
    public class EveryDayBookAndZhiXuanCangShuHelper
    {
        static string ZhiXuanFlag = "校对版全本";
        static List<string> PictureSffix = new List<string> { "bmp", "jpg", "png", "tif", "gif", "psd" };
        static List<string> AduioSffix = new List<string> { "wave", "mpeg", "mp3", "m4a" };
        static List<string> TextSffix = new List<string> { "txt", "pdf", "doc", };

        public static void ReNameFile(string oldFullPath, string newName)
        {
            //传入Url,重命名   传入一个正则 一个文件根目录 然后替换里面所有的txt文件名
            FileInfo fInfo = new FileInfo(oldFullPath);

            //回去最后一个/
            var index = oldFullPath.LastIndexOf(@"\") + 1;
            if (index <= 0)
            {
                return;
            }
            var lastIndex = oldFullPath.LastIndexOf(@".");
            var oldFileName = oldFullPath.Substring(index, lastIndex - index);
            newName = oldFullPath.Replace(oldFileName, newName);
            fInfo.MoveTo(Path.Combine(newName));
        }

        public static void ReNameFileByNewPath(string oldFullPath, string newPath)
        {
            //传入Url,重命名   传入一个正则 一个文件根目录 然后替换里面所有的txt文件名
            FileInfo fInfo = new FileInfo(oldFullPath);
            fInfo.MoveTo(Path.Combine(newPath));
        }

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
        /// 每日听本书专用
        /// </summary>
        /// <param name="url"></param>
        public static void EveryDayEveryBookSort(string dirs)
        {
            //传入Url,重命名  分为图片img jpg jpeg   音频 mp3,mpga  文本txt pdf等
            var allFileList = new List<string>();

            var pictureList = new List<string>();
            var audioList = new List<string>();
            var textList = new List<string>();
            GetDirectorAllFile(dirs, allFileList);
            //获取所有的文件进行分类
            foreach (var item in allFileList)
            {
                if (CheckIsPicture(item))
                {
                    pictureList.Add(item);
                    continue;
                }
                if (CheckIsAudio(item))
                {
                    audioList.Add(item);
                    continue;
                }
                if (CheckIsText(item))
                {
                    textList.Add(item);
                    continue;
                }
            }
            DirectoryInfo dir = new DirectoryInfo(dirs);
            var baseDir = dir.Root.FullName;
            var pictureDir = Path.Combine(baseDir, "每天听本书-图片");
            var audioDir = Path.Combine(baseDir, "每天听本书-音频");
            var textDir = Path.Combine(baseDir, "每天听本书-文本");
            //判断文件夹是否存在
            if (!Directory.Exists(pictureDir))
            {
                Directory.CreateDirectory(pictureDir);
            }
            if (!Directory.Exists(audioDir))
            {
                Directory.CreateDirectory(audioDir);
            }
            if (!Directory.Exists(textDir))
            {
                Directory.CreateDirectory(textDir);
            }


            foreach (var item in pictureList)
            {
                var newName = Path.Combine(pictureDir, GetFileName(item));
                MoveFile(item, newName);
            }
            foreach (var item in audioList)
            {
                var newName = Path.Combine(audioDir, GetFileName(item));
                MoveFile(item, newName);
            }
            foreach (var item in textList)
            {
                var newName = Path.Combine(textDir, GetFileName(item));
                MoveFile(item, newName);
            }
        }

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
        #region 验证文件类型
        public static bool CheckIsPicture(string fileName)
        {
            var sffix = GetFileSffix(fileName);
            return PictureSffix.Contains(sffix);
        }

        public static bool CheckIsAudio(string fileName)
        {
            var sffix = GetFileSffix(fileName);
            return AduioSffix.Contains(sffix);
        }
        public static bool CheckIsText(string fileName)
        {
            var sffix = GetFileSffix(fileName);
            return TextSffix.Contains(sffix);
        }
        #endregion

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

        /// <summary>
        /// 获取全路径中的文件名
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetFileName(string filePath)
        {
            var index = filePath.LastIndexOf(@"\") + 1;
            var oldFileName = filePath.Substring(index, filePath.Length - index);
            return oldFileName;
        }

        /// <summary>
        /// 更新河蟹的txt  每一行都单独整理  试一下用异步看看  分开每个100k的文件,然后最后统一合并成一个文本文件
        /// </summary>
        /// <param name="url"></param>
        public static void UpdateHeXie(string url)
        {



        }


        #region 知轩藏书专用
        /// <summary>
        /// 知轩藏书重命名单独File
        /// </summary>
        /// <param name="oldFullPath"></param>
        public static void ReNameZhiXuanFile(string oldFullPath)
        {
            //《刷钱人生》（校对版全本）作者：沈自华.txt
            FileInfo fInfo = new FileInfo(oldFullPath);

            var index = oldFullPath.LastIndexOf(@"\") + 1;
            if (index <= 0)
            {
                return;
            }
            var lastIndex = oldFullPath.LastIndexOf(@".");
            var oldFileName = oldFullPath.Substring(index, lastIndex - index);
            //处理这个最后的名字  得到新Name
            var handleFileName = oldFileName.Remove(0, 1);
            var lastMark = handleFileName.LastIndexOf(@"》");
            var newName = handleFileName.Substring(0, lastMark);
            newName = oldFullPath.Replace(oldFileName, newName);
            fInfo.MoveTo(Path.Combine(newName));
        }

        /// <summary>
        /// 知轩藏书 选中文件夹批量更新
        /// </summary>
        /// <param name="oldFullPath"></param>
        /// <param name="newName"></param>
        public static void RenameZhiXuanFolder(string dirs)
        {
            var fileInfos = new List<string>();
            //递归获取文件夹中所有文件
            GetDirectorAllFile(dirs, fileInfos);
            foreach (var item in fileInfos)
            {
                if (CheckIsZhiXuan(item))
                {
                    //重命名
                    ReNameZhiXuanFile(item);
                }
            }
        }

        /// <summary>
        /// 检测是否是知轩下载的
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool CheckIsZhiXuan(string fileName)
        {
            return fileName.Contains(ZhiXuanFlag) && fileName.Contains(".txt");
        }
        #endregion
    }
}
