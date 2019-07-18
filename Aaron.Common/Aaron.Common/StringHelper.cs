using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Aaron.Common
{
    public class StringHelper
    {

        public static string RemoveLastChar(string str)
        {
            if (str.Length>0)
            {
              return str.Remove(str.Length - 1, 1);
            }
            return "";
        }
    }
}