using Aaron.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirFileSortAndRename
{
    class Program
    {
        static void Main(string[] args)
        {
            //var dirPath = @"E:\认知方法论\";

            //FileHelper.DifferentFileSortOrderby(dirPath);
            var targetFile = @"E:\认知方法论\图片归类\";
            BaseRename model = new BaseRename();
            model.Rename(targetFile);
        }
    }



}
