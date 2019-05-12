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

            //RedisHelper redis = new RedisHelper();
            //var key = "aa";
            //redis.SetStringValue(key, "木头人");
            //var value = redis.GetStringValue(key);

            var dt = MySQLHelper.GetDataTable();
            Console.ReadLine();
        }
    }
}
