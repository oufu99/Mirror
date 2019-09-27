using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionJs
{
    public class Common
    {
        public static string FormaterJS(string jsText, string jsFileName)
        {
            jsText = jsText.Replace("\"", "!`!");
            jsText = jsText.Replace("'", "&!&");

            jsText = jsText.Replace("\\r", "\\\\r");
            jsText = jsText.Replace("\\n", "\\\\n");
            jsText = jsText.Replace("\\t", "\\\\t");

            //jsText = jsText.Replace("\\r", "");
            //jsText = jsText.Replace("\\n", "");
            //jsText = jsText.Replace("\\t", "");

            var text = "var myZpStr='" + jsText + "';  ";
            text += " myZpStr=myZpStr.replace(/!`!/g, \"\\\"\").replace(/&!&/g, \"'\");";
            text += "$(\"#source\").text(myZpStr); ";
            text += "$(\".execute\").click();";
            var time = @"var timer = setInterval(function () {
                var text = $('#resultSource').val().trim();
                if (text == '') {

                }
                else {" +
              @"
                        $('#resultSource').val('');
                        bound.handlerJs('" + jsFileName + @"',text); 
                        clearInterval(timer);
                }
            }, 1000);";
            text += time;
            return text;
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
    }
}
