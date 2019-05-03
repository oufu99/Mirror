using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitUser
{
    public class SpecialAlarm : IAlarm
    {
        public void Alarm()
        {
            Console.WriteLine("特殊警器报警啦!");
        }
    }
}
