using Aaron.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReName
{
    class Program
    {
        static void Main(string[] args)
        {
            //重命名

            //bf0410   开始  每次循环12 *  31次
            //定义一个001的种子开始

            var index = 1;
            var month = 4;

            var path = @"D:\每天听本书-图片\";
            var list = new List<string>();
            var files = IOHelper.GetDirectorAllFile(path, list);

            for (int j = 0; j < 12; j++)
            {
                for (int i = 0; i < 31; i++)
                {
                    var str = $"bf{FormatterMonth(month)}{FormatterMonth(i)}";
                    //替换掉前面几个字
                    var oldNames = list.Where(c => c.Contains(str));
                    if (oldNames.Count() > 0)
                    {
                        if (oldNames.Count() > 1)
                        {
                            var oldList = oldNames.ToList();

                            //整一个标识用来对应修改前和修改后的

                            //把广告词替换掉
                            //排序
                            oldList.Sort((x, y) => x.Length.CompareTo(y.Length));
                            for (int z = 0; z < oldList.Count; z++)
                            {
                                var oldName = oldList[z];
                                var newName = oldList[z].Replace(str, FormatterString(index)).Replace("更多课程微信know811", ""); ;
                                var oldPath = Path.Combine(path, oldName);
                                IOHelper.ReNameFileByNewPath(oldPath, newName);

                                index++;
                            }
                        }
                        else
                        {
                            var oldName = oldNames.First();
                            //测试代码
                            if (oldName.Contains("0101"))
                            {

                            }
                            var newName = oldName.Replace(str, FormatterString(index)).Replace("更多课程微信know811", "");
                            var oldPath = Path.Combine(path, oldName);
                            IOHelper.ReNameFileByNewPath(oldPath, newName);
                            index++;
                        }

                    }
                }
                if (month == 12)
                {
                    month = 0;
                }
                month++;
            }
            Console.ReadLine();
        }
        static string FormatterMonth(int index)
        {
            if (index < 10)
            {
                return $"0{index}";
            }
            return index.ToString();

        }

        static string FormatterString(int index)
        {
            if (index < 10)
            {
                return $"00{index}";
            }
            if (index < 100)
            {
                return $"0{index}";
            }
            return index.ToString();

        }
    }
}
