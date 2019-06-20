using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tfs.Common
{

    //通过cmd命令来操作,如果后面改成git 也是同样使用然后用一个工厂来生成对应的类就可以了
    public class TfHelper
    {
        /// <summary>
        /// 获取最新操作
        /// </summary>
        /// <param name="disk">硬盘符</param>
        /// <param name="dirPath">vs的安装路径</param>
        /// <param name="workArea">工作区</param>
        /// <returns></returns>
        public static string GetOpt(string disk, string dirPath, string workArea)
        {
            var strCmdList = new List<string>()
            {
                disk,
                string.Format(@"cd {0}\CommonExtensions\Microsoft\TeamFoundation\Team Explorer",dirPath),
                string.Format(@"tf get {0} /recursive",workArea)
            };
            return CmdHelper.Excute(strCmdList);
        }
        ///// <summary>
        ///// 编译项目 都是调用cmd中的命令进行编译
        ///// </summary>
        ///// <param name="buildPah">编译文件的路径</param>
        ///// <param name="outputPath">输出路径</param>
        ///// <returns></returns>
        //public static string Build(string dirPath, string buildPah, string outputPath)
        //{
        //    // msbuild E:\zp4\Common\ZP.Common.DataModels\ZP.Common.DataModels.csproj
        //    var strs = dirPath.Split(':');

        //    var strCmdList = new List<string>()
        //    {
        //        strs[0]+":",
        //        @"cd "+dirPath,
        //         string.Format(@"msbuild   {0} /p:OutputPath={1} /t:rebuild", buildPah,outputPath)
        //        //string.Format(@"msbuild   {0} /p:OutputPath={1}  /t:rebuild", buildPah,outputPath)
        //    };
        //    return CmdHelper.Excute(strCmdList);
        //}

        public async static Task<string> Build(string dirPath, string buildPah, string outputPath)
        {
            // msbuild E:\zp4\Common\ZP.Common.DataModels\ZP.Common.DataModels.csproj
            var strs = dirPath.Split(':');

            var strCmdList = new List<string>()
            {
                strs[0]+":",
                @"cd "+dirPath,
                 string.Format(@"msbuild   {0} /p:OutputPath={1} /t:rebuild", buildPah,outputPath)
                //string.Format(@"msbuild   {0} /p:OutputPath={1}  /t:rebuild", buildPah,outputPath)
            };
            return CmdHelper.Excute(strCmdList);
        }


        /// <summary>
        /// 输出到bin目录
        /// </summary>
        /// <param name="dirPath"></param>
        /// <param name="buildPah"></param>
        /// <param name="outputPath"></param>
        /// <returns></returns>
        public static string Build(string dirPath, string buildPah)
        {
            // msbuild E:\zp4\Common\ZP.Common.DataModels\ZP.Common.DataModels.csproj
            var strs = dirPath.Split(':');

            var strCmdList = new List<string>()
            {
                strs[0]+":",
                @"cd "+dirPath,
                 string.Format(@"msbuild   {0} /t:rebuild", buildPah)
            };
            return CmdHelper.Excute(strCmdList);
        }
    }
}
