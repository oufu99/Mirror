using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aaron.Common
{
    public static class StringExtends
    {
        public static string GetFirstInt(this string str)
        {
            int index = 0;
            bool flag = true;
            for (int i = 0; i < str.Length; i++)
            {
                if (flag)
                {
                    int res = -1;
                    int.TryParse(str[i].ToString(), out res);
                    if (res == 0 && str[i] != '0')
                    {
                        flag = false;
                        index = i;
                    }

                }
                else
                {
                    return str.Substring(0,index);
                }
            }
            return str.Substring(index);
        }

        public static string RemoveLastChar(this string str)
        {
            if (str.Length > 0)
            {
                return str.Remove(str.Length - 1, 1);
            }
            return "";
        }
    }
}
