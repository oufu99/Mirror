using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace ReportMH
{
    public partial class Form1 : Form
    {
        string dicPath = @"E:\Mirror\ReportMH\";
        string filePath { get { return dicPath + "举报名单.txt"; } set { } }
        string configFilePath { get { return dicPath + "配置信息.txt"; } set { } }
        List<string> resultList = new List<string>();
        List<string> myReportList = new List<string>();
        List<string> urlList = new List<string>();
        DataConfig configModel;
        DateTime beginDate;

        bool isFirst = false;
        bool isEnd = false;

        public Form1()
        {
            InitializeComponent();
            //添加一个使用更新开关
            //添加程序开始时间,为了避免程序运行过程中,11突然发布新公告  稳如狗
            //如果历史举报文件txt不存在 就把当前运行的目录赋值
            if (Directory.Exists(dicPath) == false)
            {
                dicPath = System.AppDomain.CurrentDomain.BaseDirectory;
            }
            txtName.TabIndex = 0;

            //检查是否第一次举报
            if (File.Exists(configFilePath) == false)
            {
                //之前无效
                configModel = new DataConfig() { FinallyTime = DateTime.Parse("2019-04-01") };
                File.AppendAllText(configFilePath, JsonConvert.SerializeObject(configModel));
            }
            else
            {
                //获取上一次查询的时间 用来避免无谓查询
                string configJson = File.ReadAllText(configFilePath);
                configModel = JsonConvert.DeserializeObject<DataConfig>(configJson);
            }
            if (File.Exists(filePath) == false)
            {
                isFirst = true;
                return;
            }
            //初始化举报名单
            InitText();
        }

        private void InitText()
        {
            string text = File.ReadAllText(filePath);
            myReportList = text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            for (int i = 0; i < myReportList.Count; i++)
            {
                //处理封禁符号  以免后面对不上
                myReportList[i] = myReportList[i].Replace("   已封禁", "");
            }
        }
        private void Report(object sender, EventArgs e)
        {
            var content = this.txtName.Text.Trim();
            if (string.IsNullOrEmpty(content))
            {
                MessageBox.Show("请输入要举报的名称再进行提交");
                return;
            }
            string toEmail = "chinaimba1314@163.com";
            var fromEmail = "q51758018@163.com";
            var subject = "举报";

            SendEmail(toEmail, fromEmail, subject, content);
            MessageBox.Show("发送成功");
        }

        private void SendEmail(string toEmail, string fromEmail, string subject, string content)
        {
            var smtpCode = "shouquan163";
            //实例化一个发送邮件类。
            MailMessage mailMessage = new MailMessage();
            //发件人邮箱地址，方法重载不同，可以根据需求自行选择。
            mailMessage.From = new MailAddress(fromEmail);
            //收件人邮箱地址。
            mailMessage.To.Add(new MailAddress(toEmail));
            //邮件标题。
            mailMessage.Subject = subject;
            //邮件内容。
            mailMessage.Body = content;
            //实例化一个SmtpClient类。
            SmtpClient client = new SmtpClient();
            //在这里我使用的是qq邮箱，所以是smtp.qq.com，如果你使用的是126邮箱，那么就是smtp.126.com。
            client.Host = "smtp.163.com";
            //使用安全加密连接。
            client.EnableSsl = true;
            //不和请求一块发送。
            client.UseDefaultCredentials = false;
            //验证发件人身份(发件人的邮箱，邮箱里的生成授权码);
            client.Credentials = new NetworkCredential(fromEmail, smtpCode);
            //发送
            client.Send(mailMessage);

            //在本地生成一个文件,用来查看自己举报的
            //检查文件夹是否存在
            if (isFirst)
            {
                File.AppendAllText(filePath, content);
            }
            else
            {
                //避免第一行是空行,代码洁癖啊
                string text = File.ReadAllText(filePath);
                if (string.IsNullOrEmpty(text))
                {
                    File.AppendAllText(filePath, content);
                }
                else
                {
                    File.AppendAllText(filePath, "\r\n" + content);
                }
            }
        }

        private void OpenTxtFile(object sender, EventArgs e)
        {
            if (isFirst)
            {
                MessageBox.Show("您还没有进行过举报");
                return;
            }
            OpenText();
        }

        private void OpenText()
        {
            System.Diagnostics.Process.Start("NotePad.exe", filePath);
        }

        private void CheckReportResult(object sender, EventArgs e)
        {
            //记录本次开始的时间
            beginDate = DateTime.Now;
            if (isFirst)
            {
                MessageBox.Show("您还没有进行过举报");
                return;
            }
            //防止这次举报的名单丢失
            InitText();
            this.btnReport.Text = "查询中...";

            string text = File.ReadAllText(filePath);
            if (string.IsNullOrEmpty(text))
            {
                MessageBox.Show("您还没有进行过举报");
                return;
            }
            CheckMHMain();
            this.btnReport.Text = "查看举报结果";
            OpenText();
        }

        /// <summary>
        /// 爬官方的举报信息   获取首页
        /// </summary>
        private void CheckMHMain()
        {
            //从第一页开始访问 只要标题是这个就直接结束循环
            //把结果存在电脑上   添加一个最后访问时间,如果时间小于这个就不用查了

            //清空记录
            string mainUrl = "https://cmsapi.5211game.com/NewsService/YYService/YYNews.ashx?op=NewsListByPage&PageSize=10&PageIndex=1&CategoryIds=2&itemIds=4,12,71";
            //记录时间遍历子项
            WebClient wc = new WebClient();//创建WebClient对象
            Stream s = wc.OpenRead(mainUrl);//访问网址并用一个流对象来接受返回的流
            StreamReader sr = new StreamReader(s, Encoding.UTF8);
            string htmlStr = sr.ReadToEnd();
            //string htmlStr = File.ReadAllText(@"d:\jialin.txt");
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(htmlStr);
            List<string> urlList = new List<string>();
            JObject obj = JObject.Parse(htmlStr);
            var newsCount = int.Parse(obj["NewsCount"].ToString());
            for (int i = 1; (i < newsCount / 10) && !isEnd; i++)
            {
                var pageUrl = "https://cmsapi.5211game.com/NewsService/YYService/YYNews.ashx?op=NewsListByPage&PageSize=10&PageIndex=" + i + "&CategoryIds=2&itemIds=4,12,71";
                GetPageData(pageUrl);
            }
            //最后处理所有符合条件的url
            GetFinallyData();
        }

        /// <summary>
        /// 每次分页返回后调用
        /// </summary>
        /// <param name="htmlStr"></param>
        private void GetPageData(string url)
        {
            WebClient wc = new WebClient();//创建WebClient对象
            Stream s = wc.OpenRead(url);//访问网址并用一个流对象来接受返回的流
            StreamReader sr = new StreamReader(s, Encoding.UTF8);
            HtmlDocument doc = new HtmlDocument();
            string htmlStr = sr.ReadToEnd();
            doc.LoadHtml(htmlStr);
            JObject obj = JObject.Parse(htmlStr);
            var t = obj["NewsList"];
            foreach (var mainItem in obj)
            {
                if (mainItem.Key == "NewsList")
                {
                    for (int i = 0; i < mainItem.Value.Count(); i++)
                    {
                        var title = mainItem.Value[i]["News_Title"].ToString();
                        if (title.Contains("反外挂公会"))
                        {
                            urlList.Add(mainItem.Value[i]["News_URL"].ToString());
                            string dateStr = mainItem.Value[i]["AddDate"].ToString();
                            var date = DateTime.Parse(dateStr);
                            if (configModel.FinallyTime > date)
                            {
                                isEnd = true;
                                return;
                            }
                        }
                    }
                }
            }

        }

        private void GetFinallyData()
        {
            //记录时间遍历子项
            foreach (var item in urlList)
            {
                WebClient wc = new WebClient();//创建WebClient对象
                Stream s = wc.OpenRead(item);//访问网址并用一个流对象来接受返回的流
                StreamReader sr = new StreamReader(s, Encoding.UTF8);//
                string htmlStr = sr.ReadToEnd();
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(htmlStr);
                //这里是只要匹配到就都能得出来
                var nodes = doc.DocumentNode.SelectNodes(@"//td");
                if (nodes != null || nodes.Count >= 0)
                {
                    //提取出a标签的值
                    foreach (var node in nodes)
                    {
                        var name = node.InnerText;
                        if (myReportList.Contains(name))
                        {
                            var index = myReportList.IndexOf(name);
                            myReportList[index] = name + "   已封禁";
                        }
                    }
                }
            }
            //更新举报名单  后面加上封禁标志
            File.WriteAllText(filePath, myReportList[0]);
            for (int i = 1; i < myReportList.Count; i++)
            {
                File.AppendAllText(filePath, "\r\n" + myReportList[i]);
            }

            //更新时间
            configModel.FinallyTime = beginDate;
            var json = JsonConvert.SerializeObject(configModel);
            File.WriteAllText(configFilePath, json);
        }
    }
}
