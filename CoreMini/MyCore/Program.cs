using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCore
{

    public delegate Task MyDelegate(Context context);
    class Program
    {
     
        static void Main(string[] args)
        {
            //模拟一个中间件
        }
    }

    public class Context
    {
        public string Request { get; set; }
        public string Respone { get; set; }
        public void Write(string text)
        {
            this.Respone += text;
        }
    }

}
