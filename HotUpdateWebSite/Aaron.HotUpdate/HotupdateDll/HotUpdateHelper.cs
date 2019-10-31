using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Aaron.HotUpdate
{
    public class HotUpdateHelper
    {
        public static string GetAssemblyFullPath(string dllName)
        {
            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
            return Path.Combine(basePath, dllName);
        }
    }
}
