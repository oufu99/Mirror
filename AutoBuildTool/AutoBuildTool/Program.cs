using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tfs.Common;

namespace AutoBuildTool
{
    class Program
    {
        static void Main(string[] args)
        {


            Console.WriteLine("-----------------------------开始获取文件最新版本-------------------------------------------");


            string workArea = ConfigurationManager.AppSettings["workArea"];//工作区
            var dirPath = ProcessHelper.GetInstallDirName("devenv");//vs的安装路径

            var disk = dirPath.Split(new char[] { '\\' })[0];//硬盘符
                                                             //获取最新操作
            var str = TfHelper.GetOpt(disk, dirPath, workArea);
            Console.WriteLine(str);

            Console.WriteLine("-----------------------------开始编译-------------------------------------------");

            //进行编译
            var dirkPath = @"C:\Windows\Microsoft.NET\Framework\v4.0.30319";

            var filePath= Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Project.txt");
            var fileStr=  File.ReadAllLines(filePath);//要更新的项目
            var outPath = Path.Combine(workArea.Substring(0, workArea.LastIndexOf('\\')), @"lib");//工作区的上一级建立lib文件夹
            if (Directory.Exists(outPath))
            {
                Directory.Delete(outPath, true);
                Directory.CreateDirectory(outPath);
            }
            string projectPath = "";
            Console.WriteLine(string.Join("," ,fileStr));
            foreach (var item in fileStr)
            {
                projectPath = Path.Combine(workArea, item);
                Console.WriteLine("**************编译结果！！*************************");
                var buildRstStr = TfHelper.Build(dirkPath, projectPath, outPath);
                Regex reg = new Regex(@"[0-9]+ 个错误", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                var rst = reg.Match(buildRstStr);
                if (rst != null && rst.Value.Length > 0)
                {
                    var count = rst.Value.Split('个')[0];
                    if (string.IsNullOrWhiteSpace(count) || Convert.ToInt32(count) > 0)
                    {
                        Console.WriteLine("**************编译失败！！*************************");
                        Console.WriteLine("项目："+ projectPath+",编译失败，编译结束，请检查，错误如下，看不懂就手动自己生成试试");
                        Console.WriteLine(buildRstStr);
                        Console.WriteLine("**************编译失败！！*************************");

                    }
                    else
                    {
                        Console.WriteLine("项目：" + projectPath + "，生成成功");
                    }
                }
            }
            Console.WriteLine("生成结束");
            Console.ReadLine();
        }

    }
}
