﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Utils
    {
        public static readonly string ConnStr = ConfigurationManager.AppSettings["Conn"];
    }
}
