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
        #region 文件相关
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
        public static string GetHouZhui(string fileName)
        {
            var lastIndex = fileName.LastIndexOf(@".") + 1;
            if (lastIndex <= 0)
            {
                return "";
            }
            return fileName.Substring(lastIndex, fileName.Length - lastIndex);
        }

        /// <summary>
        /// 根据全路径获取 文件名
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetFileNameByFullPath(string filePath)
        {
            //替换所有/ 为\
            filePath = filePath.Replace(@"/", @"\");
            var lastIndex = filePath.LastIndexOf(@"\") + 1;
            if (lastIndex <= 0)
            {
                return "";
            }
            return filePath.Substring(lastIndex, filePath.Length - lastIndex);
        }

        /// <summary>
        /// 根据全路径获取文件路径(除了文件名的其他信息)
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetFilePathByFullPath(string filePath)
        {
            //替换所有/ 为\
            filePath = filePath.Replace(@"/", @"\");
            var lastIndex = filePath.LastIndexOf(@"\") + 1;
            if (lastIndex <= 0)
            {
                return "";
            }
            return filePath.Substring(0, lastIndex);
        }

        /// <summary>
        /// 根据全路径获取盘符
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetDiskNameByFullPath(string filePath)
        {
            //替换所有/ 为\
            filePath = filePath.Replace(@"/", @"\");
            var lastIndex = filePath.IndexOf(@"\");
            if (lastIndex <= 0)
            {
                return "";
            }
            return filePath.Substring(0, lastIndex);
        }
        public static bool CheckFileIsExist(string path)
        {
            return File.Exists(path);
        }

        /// <summary>
        /// 打开文件  
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="arg">如果需要传递多个参数 以空格隔开  "arg1 arg2"</param>
        public static void OpenSoft(string fullPath, string arg = "")
        {
            Process.Start(fullPath, arg);
        }

        /// <summary>
        /// 启动参数这里一般都是项目名称 如OpenMyTools 会再编译一遍再重新打开
        /// /// </summary>
        /// <param name="projectName"></param>
        public static void ReloadSoftByProjectName(string projectName)
        {
            ReloadProject(projectName);
        }

        /// <summary>
        /// 传入全路径,就是相当于重新打开
        /// </summary>
        /// <param name="fullPath"></param>
        public static void ReloadSoftByFullPath(string fullPath)
        {
            var parm = "HaveFullPath " + "\"" + fullPath + "\""; //参数通过空格进行分隔
            ReloadProject(parm);
        }

        private static void ReloadProject(string parm)
        {
            //那个加载项目的地址  这里直接写地址,后面就能直接用
            string reloadProjectPath = @"D:\Tools\ReLoadProject\bin\Debug\ReLoadProject.exe";
            string buildPath = @"D:\Tools\ReLoadProject\ReLoadProject.csproj";
            if (CheckFileIsExist(reloadProjectPath))
            {
                AutoBuildHelper.BuildOutBin(buildPath);
            }
            //判断一下目录是否存在,如果不存在就重新编译一下再打开
            Process.Start(reloadProjectPath, parm);
        }

        public static byte[] GetFileData(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                byte[] buffur = new byte[fs.Length];
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    bw.Write(buffur);
                    bw.Close();
                }
                return buffur;
            }
        }
        #endregion

        #region 文件夹相关
        public static void CopyDirectory(string srcPath, string targetPath, bool isCover = true)
        {
            //先判断一下目标文件夹是否存在
            CreateDirectory(targetPath);
            //循环子文件夹
            DirectoryInfo dir = new DirectoryInfo(srcPath);
            FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //获取目录下（不包含子目录）的文件和子目录
            foreach (FileSystemInfo file in fileinfo)
            {
                if (file is DirectoryInfo)   //判断是否文件夹
                {
                    CreateDirectory(targetPath + "\\" + file.Name);
                    CopyDirectory(file.FullName, targetPath + "\\" + file.Name);    //递归调用复制子文件夹
                }
                else
                {
                    File.Copy(file.FullName, targetPath + "\\" + file.Name, isCover);      //不是文件夹即复制文件，true表示可以覆盖同名文件
                }
            }
        }
        public static void CopyFile(string srcPath, string targetPath, bool isCover = true)
        {
            File.Copy(srcPath, targetPath, isCover);
            //不是文件夹即复制文件，true表示可以覆盖同名文件
        }

        public static void CreateDirectory(string dirPath)
        {
            if (!CheckDirIsExist(dirPath))
            {
                Directory.CreateDirectory(dirPath);   //目标目录下不存在此文件夹即创建子文件夹
            }
        }
        public static void CleanDirectory(string dirPath)
        {
            if (!CheckDirIsExist(dirPath))
            {
                Directory.CreateDirectory(dirPath);   //目标目录下不存在此文件夹即创建子文件夹
            }
            else
            {
                DeleteDirectory(dirPath);
                Directory.CreateDirectory(dirPath);
            }
        }
        public static void DeleteDirectory(string dirPath)
        {
            DirectoryInfo dir = new DirectoryInfo(dirPath);
            if (dir.GetDirectories().Length == 0 && dir.GetFiles().Length == 0)
            {
                //如果文件夹下没有文件和文件夹就删除
                dir.Delete();
                return;
            }
            foreach (DirectoryInfo d in dir.GetDirectories())
            {
                DeleteDirectory(d.FullName);
            }
            foreach (FileInfo fi in dir.GetFiles())
            {
                fi.Delete();
            }
            dir.Delete(true);
        }

        private static bool CheckDirIsExist(string dirPath)
        {
            return Directory.Exists(dirPath);
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
        /// 根据传入的路径 获取向前几层
        /// </summary>
        /// <param name="path"></param>
        /// <param name="dirCount"></param>
        /// <returns></returns>
        public static string GetParentPath(string path, int dirCount, bool isContainFileName = false)
        {
            if (string.IsNullOrEmpty(path))
            {
                return "";
            }
            var temp = path.Replace(@"/", @"\");
            var list = new List<int>();
            var index = 0;
            while (index != -1)
            {
                var tempIndex = index + 1;
                index = temp.IndexOf(@"\", tempIndex);
                if (index > 0)
                {
                    list.Add(index);
                }
            }
            var res = temp.Substring(0, list[list.Count - dirCount]);
            if (isContainFileName)
            {
                var fileName = GetFileNameByFullPath(path);
                return Path.Combine(res, fileName);
            }
            return res;
        }



        #endregion

    }
}
