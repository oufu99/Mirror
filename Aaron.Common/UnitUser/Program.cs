﻿using Aaron.Common;
using Aaron.DataCommon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UnitUser
{
    class Program
    {

        static void Main(string[] args)
        {

            RedisHelper helper = new RedisHelper();
            helper.Set("aa","aaron");
            Console.WriteLine(helper.Get("aa"));


            Console.ReadLine();



        }
    }
}
