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

        BoundObject obj;
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public ChromiumWebBrowser browser;

        public void InitBrowser()
        {

            var setting = new CefSharp.CefSettings();
            setting.CefCommandLineArgs.Add("disable-gpu", "1"); // 禁用gpu
            Cef.Initialize(setting, true, false);
            browser = new ChromiumWebBrowser("https://www.sojson.com/jsobfuscator.html");
            this.Controls.Add(browser);
            browser.Dock = DockStyle.Fill;

            obj = new BoundObject(browser);
            browser.RegisterJsObject("bound", obj);
            d1 = DateTime.Now;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (obj.fileNameList.Count == 0)
            {
                MessageBox.Show("请先设置含有js文件的路径");
                return;
            }

            if (fileNameList.Count > 0)
            {
                var jsPath = fileNameList.First();
                fileNameList.Remove(jsPath);
                ////判断文件大于200Kb就不压缩
                FileInfo info = new FileInfo(jsPath);
                //字节为单位  大于200k
                if ((info.Length / 1000) > 200)
                {

                }
                else
                {
                    var name = jsPath.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries).Last();
                    var jsText = GetAllText(jsPath);
                    var text = Common.FormaterJS(jsText, name);
                    browser.GetMainFrame().Browser.MainFrame.ExecuteJavaScriptAsync(text);
                }
            }
        }

        public string GetAllText(string jsPath)
        {
            //var text = File.ReadAllText(jsPath);

            StreamReader reader = new StreamReader(jsPath);
            //string text = reader.ReadToEnd();

            string text = "";
            string textLine = "";
            while (!string.IsNullOrEmpty(textLine = reader.ReadLine()))
            {
                text += textLine;
            }
            reader.Close();

            return text;
        }

        /// <summary>
        /// 设置路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {

            var dirPath = txtPathInput.Text;
            if (!string.IsNullOrEmpty(dirPath))
            {
                fileNameList = new List<string>();
                fileNameList = Common.GetDirectorAllFile(dirPath, fileNameList);
                obj.UpdatePath(fileNameList);
            }
            var outPath = txtPathOutput.Text;
            if (!string.IsNullOrEmpty(outPath))
            {
                obj.UpdateOutputPath(outPath);
            }
            MessageBox.Show("设置成功");
        }
    }

    public class BoundObject
    {
        public List<string> fileNameList = new List<string>();
        public List<string> failList = new List<string>();
        public ChromiumWebBrowser browser;
        public string OutputPath { get; set; }

        public BoundObject(ChromiumWebBrowser _browser)
        {
            browser = _browser;
            OutputPath = @"D:\转换后的js\";
        }
        public void HandlerJs(string fileName, string resultJSStr)
        {
            if (!Directory.Exists(OutputPath))
            {
                Directory.CreateDirectory(OutputPath);   //目标目录下不存在此文件夹即创建子文件夹
            }
            if (resultJSStr == "脚本错误：Error: Line 1: Unexpected end of input")
            {

                failList.Add(fileName);
            }
            else
            {
                FileStream fs = new FileStream(Path.Combine(OutputPath, fileName), FileMode.Create);
                StreamWriter wr = null;
                wr = new StreamWriter(fs);
                wr.WriteLine(resultJSStr);
                wr.Close();
            }
            if (fileNameList.Count > 0)
            {
                var jsPath = fileNameList.First();
                fileNameList.Remove(jsPath);
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
            else
            {
                MessageBox.Show("加载完毕,加密失败的文件有" + string.Join(",", failList) + " 请手动加密");
            }

        }

        public bool UpdatePath(List<string> _fileNameList)
        {
            fileNameList = _fileNameList;
            return true;
        }

        public bool UpdateOutputPath(string _outputPaht)
        {
            OutputPath = _outputPaht;
            return true;
        }
    }
}
