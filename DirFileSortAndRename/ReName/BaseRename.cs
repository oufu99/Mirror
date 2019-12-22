using Aaron.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirFileSortAndRename
{
    public class BaseRename
    {
        //通用分类,后面有新增的直接覆盖
        List<string> fileNameList = new List<string>();
        /// <summary>
        /// 传入需要分类的文件夹
        /// </summary>
        /// <param name="dirPath"></param>
        public virtual void Rename(string dirPath)
        {
            //bf0410   开始  每次循环12 *  31次
            //定义一个001的种子,命名的时候从001-xx 002-xx这种格式开始
            var index = 1;

            //开始的月份
            var month = 4;

            var isFirst = true;
            //此方法会给传入的List赋值
            var files = FileHelper.GetDirectorAllFile(dirPath, fileNameList);
            //为了每次遍历完31天,把月数加1,如果是放在for中,就没有合适的终止条件也不好
            while (fileNameList.Count > 0)
            {
                //一个月最多31天,哪怕多设也不会出问题
                for (int i = 0; i <= 31; i++)
                {
                    if (isFirst)
                    {
                        //上一年的4月10号开始
                        i = 10;
                        isFirst = false;
                    }
                    //预测的旧名字
                    var oldTargetName = $"bf{FormatterMonth(month)}{FormatterMonth(i)}";
                    //替换掉前面几个字
                    var oldNames = fileNameList.Where(c => c.Contains(oldTargetName));
                    if (oldNames.Count() > 0)
                    {
                        //针对图片分成1,2,3这种  排序一下
                        if (oldNames.Count() > 1)
                        {
                            var oldList = oldNames.ToList();
                            //整一个标识用来对应修改前和修改后的

                            //把广告词替换掉
                            //排序
                            oldList.Sort((x, y) => x.Length.CompareTo(y.Length));
                            //遍历所有的同名文件,1,2,3这种
                            for (int z = 0; z < oldList.Count; z++)
                            {
                                var oldName = oldList[z];
                                //因为下面的代码一模一样,所以封装一下
                                Rename(oldName, index, oldTargetName, dirPath);
                            }
                            index++;
                        }
                        else
                        {
                            var oldName = oldNames.First();
                            Rename(oldName, index, oldTargetName, dirPath);
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
        }

        void Rename(string oldName, int index, string oldTargetName, string dirPath)
        {
            var replaceWord = FormatterString(index);
            if (!oldName.Contains("-"))
            {
                replaceWord = replaceWord + "-";
            }
            var newName = oldName.Replace(oldTargetName, replaceWord).Replace("更多课程微信know811", "");
            var oldPath = Path.Combine(dirPath, oldName);
            fileNameList.Remove(oldName);
            FileHelper.ReNameFileByNewPath(oldPath, newName);
        }

        string FormatterMonth(int index)
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
