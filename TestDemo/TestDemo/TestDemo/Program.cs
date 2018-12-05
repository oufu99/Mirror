using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            OrderBLL.OrderDA bll = new OrderBLL.OrderDA();
          
            Console.ReadLine();
        }
    }

    public class NotifyPropertyChangedcs
    {

        //循环监听
        //判断文本中文字是否改变
        public event Action CheckFunc;

        public void CheckText()
        {
            var text = System.IO.File.ReadAllText(@"D:\test1.txt");
            if (text != "text")
            {
                Console.WriteLine("文件已经改变");
            }
        }

        public void SetFunc()
        {
            CheckFunc += CheckText;
        }

        public void WhileCheck()
        {
            while (true)
            {
                CheckFunc?.Invoke();
                Thread.Sleep(500);
            }
        }

    }
}
