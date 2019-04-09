using Aaron.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitUser
{
    class Program
    {
        static void Main(string[] args)
        {
            //var stamp = TimeStampHelper.GetTimestamp();

            string stamp = "1554781273";
            var date = TimeStampHelper.StampToDateTime(stamp);
            Console.ReadLine();
        }
    }
}
