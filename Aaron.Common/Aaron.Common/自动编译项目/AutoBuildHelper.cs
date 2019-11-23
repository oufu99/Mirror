using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aaron.Common
{
    /// <summary>
    /// C#自动编译类
    /// </summary>
    public class AutoBuildHelper
    {
        static string dirPath = @"C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\MSBuild\15.0\Bin";

      

        /// <summary>
        /// 传入需要编译的项目地址和输出地址,用于自定义
        /// </summary>
        /// <param name="buildPah"></param>
        /// <param name="outputPath"></param>
        /// <returns></returns>
        public static string Build(string buildPah, string outputPath)
        {
            // msbuild E:\zp4\Common\ZP.Common.DataModels\ZP.Common.DataModels.csproj
            var strs = dirPath.Split(':');
            var strCmdList = new List<string>()
            {
                strs[0]+":",
                @"cd "+dirPath,
                 string.Format(@"msbuild   {0} /p:OutputPath={1} /t:rebuild", buildPah,outputPath)
            };
            return CMDHelper.Excute(strCmdList);
        }


        /// <summary>
        /// 输出到bin目录
        /// </summary>
        /// <param name="buildPath"></param>
        /// <param name="buildPath"></param>
        /// <param name="outputPath"></param>
        /// <returns></returns>
        public static string BuildOutBin(string buildPath, bool isNetCore = false)
        {
            var binPath = FileHelper.GetParentPath(buildPath, 1) + @"\bin\Debug\";
            if (isNetCore)
            {
                binPath = binPath + @"netcoreapp2.1\";
            }
            var strs = dirPath.Split(':');
            var strCmdList = new List<string>()
            {
                strs[0]+":",
                @"cd "+dirPath,
                  string.Format(@"msbuild   {0} /p:OutputPath={1} /t:rebuild", buildPath,binPath)
            };
            return CMDHelper.Excute(strCmdList);
        }


    }
}
