using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UnitUser
{
    public class Heater
    {
        //发布订阅模式,执行权在发布者
        public List<IAlarm> AlarmList = new List<IAlarm>();
        private int Temperature = 0;
        //每次检测 超过90度就报警
        public void Checking()
        {
            while (true)
            {
                if (Temperature >= 90)
                {
                    foreach (var item in AlarmList)
                    {
                        item.Alarm();
                    }
                    return;
                }
                foreach (var item in AlarmList)
                {
                    item.Alarm();
                }
                Temperature++;
                Thread.Sleep(10);
                Console.WriteLine($"烧水中...现在温度为{Temperature}");
            }
        }

    }
}
