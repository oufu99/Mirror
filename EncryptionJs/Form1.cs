using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncryptionJs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitBrowser();
        }

        List<string> fileNameList = new List<string>();
        DateTime d1 = DateTime.Now;
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public ChromiumWebBrowser browser;

        public void InitBrowser()
        {
            var dirPath = @"D:\js";
            fileNameList = GetDirectorAllFile(dirPath, fileNameList);

            var setting = new CefSharp.CefSettings();
            setting.CefCommandLineArgs.Add("disable-gpu", "1"); // 禁用gpu
            Cef.Initialize(setting, true, false);
            browser = new ChromiumWebBrowser("https://www.sojson.com/jsobfuscator.html");
            this.Controls.Add(browser);
            browser.Dock = DockStyle.Fill;
            browser.RegisterJsObject("bound", new BoundObject(fileNameList, browser));
            d1 = DateTime.Now;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (fileNameList.Count > 0)
            {
                var jsPath = fileNameList.First();
                ////判断文件大于200Kb就不压缩
                FileInfo info = new FileInfo(jsPath);
                //字节为单位  大于200k
                if ((info.Length / 1000) > 200)
                {

                }
                else
                {
                    var name = jsPath.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries).Last();
                    var jsText = File.ReadAllText(jsPath);
                    var text = Common.FormaterJS(jsText, name);
                    browser.GetMainFrame().Browser.MainFrame.ExecuteJavaScriptAsync(text);
                }
            }
        }


        public List<string> GetDirectorAllFile(string dirs, List<string> list)
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
        /// 测试方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            var text = "bound.myMethod('111','222');";
            browser.GetMainFrame().Browser.MainFrame.ExecuteJavaScriptAsync(text);
        }
    }

    public class BoundObject
    {
        List<string> fileNameList = new List<string>();
        public ChromiumWebBrowser browser;

        public BoundObject(List<string> _fileNameList, ChromiumWebBrowser _browser)
        {
            fileNameList = _fileNameList;
            browser = _browser;
        }
        public bool Success { get; set; }
        public string MyProperty { get; set; }
        public void MyMethod(string fileName, string str)
        {

            string dirPath = @"E:\转换后的js\";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);   //目标目录下不存在此文件夹即创建子文件夹
            }
            FileStream fs = new FileStream(Path.Combine(dirPath, fileName), FileMode.Create);
            StreamWriter wr = null;
            wr = new StreamWriter(fs);
            wr.WriteLine(str);
            wr.Close();
            if (fileNameList.Count > 0)
            {
                var jsPath = fileNameList.First();
                ////判断文件大于200Kb就不压缩
                FileInfo info = new FileInfo(jsPath);
                //字节为单位  大于200k
                if ((info.Length / 1000) > 200)
                {

                }
                else
                {
                    var name = jsPath.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries).Last();
                    var jsText = File.ReadAllText(jsPath);
                    var text = Common.FormaterJS(jsText, name);
                    browser.GetMainFrame().Browser.MainFrame.ExecuteJavaScriptAsync(text);
                }
            }
        }
    }
}
