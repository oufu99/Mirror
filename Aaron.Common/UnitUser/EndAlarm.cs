using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitUser
{
    public class EndAlarm : IAlarm
    {
        public void Alarm()
        {
            Console.WriteLine("最终报警器,报警啦!");
        }
    }
}
