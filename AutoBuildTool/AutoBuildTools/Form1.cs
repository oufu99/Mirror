using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tfs.Common;

namespace AutoBuildTool
{
    public partial class Form1 : Form
    {
        string dirkPath = ConfigurationManager.AppSettings["dirkPath"];
        public Form1()
        {
            InitializeComponent();
        }

        public void BuildWsBg()
        {
            //获取最新代码
            string workArea = ConfigurationManager.AppSettings["workArea"];
            var str = GetNewCode(workArea);

            Console.WriteLine("-----------------------------开始编译-------------------------------------------");
            //进行编译
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Project.txt");
            var fileStr = File.ReadAllLines(filePath);//要更新的项目
            var outPath = Path.Combine(workArea.Substring(0, workArea.LastIndexOf('\\')), @"lib");//工作区的上一级建立lib文件夹
            if (Directory.Exists(outPath))
            {
                Directory.Delete(outPath, true);
                Directory.CreateDirectory(outPath);
            }
            string projectPath = "";
            Console.WriteLine(string.Join(",", fileStr));
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
                        Console.WriteLine("项目：" + projectPath + ",编译失败，编译结束，请检查，错误如下");
                        Console.WriteLine(buildRstStr);
                        Console.WriteLine("**************编译失败！！*************************");
                    }
                    else
                    {
                        Console.WriteLine("项目：" + projectPath + "，生成成功");
                    }
                }
            }
        }

        private string GetNewCode(string workArea)
        {
            var dirPath = ProcessHelper.GetInstallDirName("devenv");//vs的安装路径
            var disk = dirPath.Split(new char[] { '\\' })[0];//硬盘符
            Console.WriteLine("-----------------------------开始获取最新代码-------------------------------------------");
            var str = TfHelper.GetOpt(disk, dirPath, workArea);
            Console.WriteLine("-----------------------------获取最新完毕-------------------------------------------");
            return str;

        }
        private void suyaBuild(object sender, EventArgs e)
        {
            //获取最新代码
            string workArea = ConfigurationManager.AppSettings["suyaWorkArea"];
            var str = GetNewCode(workArea);

            //单独编译点击的项目
            BuildWsBg();
            var targetBasePath = ConfigurationManager.AppSettings["suyaTargetPath"];
            var targetPath = targetBasePath + @"\SuYa.Mobile.csproj";
            var targetOutPath = targetBasePath + @"\bin";
            var targetBuildResultStr = TfHelper.Build(dirkPath, targetPath, targetOutPath);
            MessageBox.Show("suya生成成功");
            Console.WriteLine(targetBuildResultStr);
        }

        /// <summary>
        /// 生成WsBg项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wsBgBuild(object sender, EventArgs e)
        {
            //单独编译点击的项目
            BuildWsBg();
            var targetBasePath = ConfigurationManager.AppSettings["wsTargetPath"];
            var targetPath = targetBasePath + @"\WsBg.Web.csproj";
            var targetOutPath = targetBasePath + @"\bin";
            var targetBuildResultStr = TfHelper.Build(dirkPath, targetPath, targetOutPath);
            MessageBox.Show("WsBg生成成功");
            Console.WriteLine(targetBuildResultStr);
        }

        private void mifeiBuild(object sender, EventArgs e)
        {
            //获取最新代码
            string workArea = ConfigurationManager.AppSettings["mifeiWorkArea"];
            var str = GetNewCode(workArea);

            //单独编译点击的项目
            BuildWsBg();
            var targetBasePath = ConfigurationManager.AppSettings["mifeiTargetPath"];
            var targetPath = targetBasePath + @"\MiFei.Mobile.csproj";
            var targetOutPath = targetBasePath + @"\bin";
            var targetBuildResultStr = TfHelper.Build(dirkPath, targetPath, targetOutPath);
            MessageBox.Show("mifei生成成功");
            Console.WriteLine(targetBuildResultStr);
        }
    }
}
