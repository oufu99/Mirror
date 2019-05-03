using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitUser
{
    public class CommonAlarm : IAlarm
    {
        public void Alarm()
        {
            Console.WriteLine("通用报警器报警啦!");
        }
    }
}
