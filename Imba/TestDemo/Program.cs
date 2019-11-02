using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //从100攻速  0.17开始   计算加80%攻速,多减0.3间隔的临界点

            double gs = 80;
            double jg = 1.5;


            double gs2 = 0;
            double jg2 = 1.2;


            var result = ((100 + gs) * 0.01) / jg;
            var result2 = ((100 + gs2) * 0.01) / jg2;
            while (result > result2)
            {
                gs2 += 1;
                result2 = ((100 + gs2) * 0.01) / jg2;
                Console.WriteLine(result2);
            }
            Console.WriteLine(gs2);


            Console.ReadLine();
        }
    }
}
