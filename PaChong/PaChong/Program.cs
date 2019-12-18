using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PaChong
{
    class Program
    {
       static void Main(string[] args)
        {

            string strContent = string.Empty;
            string path = @"D:/南怀瑾.txt";
            using (FileStream fsFile = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (StreamWriter sw = new StreamWriter(fsFile, Encoding.UTF8))
                {
                    for (int i = 5032; i <= 5099; i++)
                    {
                        string url = " http://www.shixiu.net/nanshi/zhuzuo/lyjjz/" + i + ".html";
                        WebClient wc = new WebClient(); //创建WebClient对象
                        wc.Headers.Add("Content-Type", "application/json;charset=gbk");
                        //wc.Headers.Add("Accept", "application/json;charset=utf-8");
                        Stream s = wc.OpenRead(url); //访问网址并用一个流对象来接受返回的流
                        StreamReader sr = new StreamReader(s, Encoding.GetEncoding("GBK")); //
                        string htmlStr = sr.ReadToEnd();
                        HtmlDocument doc = new HtmlDocument();
                        doc.LoadHtml(htmlStr);
                        var nodes = doc.DocumentNode.SelectNodes(@"//p");
                        foreach (var item in nodes)
                        {
                            string target = item.InnerText;
                            string tempStr = Regex.Replace(target, "[&].+?[;]", "");
                            sw.WriteLine(tempStr);
                        }
                    }
                }
            }

            Console.WriteLine("执行完毕");
            Console.ReadLine();
        }
    }
}
