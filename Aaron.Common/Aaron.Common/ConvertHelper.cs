using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aaron.Common
{
    public class ConvertHelper
    {
        public static Hashtable ObjectConvertHash<T>(T t)
        {
            Hashtable hash = new Hashtable();
            var type = typeof(T);
            var instance = Activator.CreateInstance(type);
            var fields = type.GetProperties();
            foreach (var item in fields)
            {
                hash.Add(item.Name, item.GetValue(t));
            }
            return hash;
        }

    }
}
