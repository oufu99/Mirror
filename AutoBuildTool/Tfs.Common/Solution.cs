using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tfs.Common
{
    public class Solution
    {
        //internal class SolutionParser
        //Name: Microsoft.Build.Construction.SolutionParser
        //Assembly: Microsoft.Build, Version=4.0.0.0

        Type s_SolutionParser;
        PropertyInfo s_SolutionParser_solutionReader;
        MethodInfo s_SolutionParser_parseSolution;
        PropertyInfo s_SolutionParser_projects;

        //static Solution()
        //{
        //    //s_SolutionParser_projects.GetValue()
        //    s_SolutionParser = Type.GetType("Microsoft.Build.Construction.SolutionParser, Microsoft.Build, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", false, false);
        //    if (s_SolutionParser != null)
        //    {
        //        s_SolutionParser_solutionReader = s_SolutionParser.GetProperty("SolutionReader", BindingFlags.NonPublic | BindingFlags.Instance);
        //        s_SolutionParser_projects = s_SolutionParser.GetProperty("Projects", BindingFlags.NonPublic | BindingFlags.Instance);
        //        s_SolutionParser_parseSolution = s_SolutionParser.GetMethod("ParseSolution", BindingFlags.NonPublic | BindingFlags.Instance);
        //    }
        //}

        public List<SolutionProject> Projects { get; private set; }
        public List<SolutionConfiguration> Configurations { get; private set; }

        public string SolutionName { get; private set; }

        public Solution(string solutionFileName)
        {
            s_SolutionParser = Type.GetType("Microsoft.Build.Construction.SolutionParser, Microsoft.Build, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", false, false);
            if (s_SolutionParser != null)
            {
                s_SolutionParser_solutionReader = s_SolutionParser.GetProperty("SolutionReader", BindingFlags.NonPublic | BindingFlags.Instance);
                s_SolutionParser_projects = s_SolutionParser.GetProperty("Projects", BindingFlags.NonPublic | BindingFlags.Instance);
                s_SolutionParser_parseSolution = s_SolutionParser.GetMethod("ParseSolution", BindingFlags.NonPublic | BindingFlags.Instance);
            }
            if (s_SolutionParser == null)
            {
                throw new InvalidOperationException("Can not find type 'Microsoft.Build.Construction.SolutionParser' are you missing a assembly reference to 'Microsoft.Build.dll'?");
            }
            var solutionParser = s_SolutionParser.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic).First().Invoke(null);
            using (var streamReader = new StreamReader(solutionFileName))
            {
                s_SolutionParser_solutionReader.SetValue(solutionParser, streamReader, null);
                s_SolutionParser_parseSolution.Invoke(solutionParser, null);
            }
            var projects = new List<SolutionProject>();
            var array = (Array)s_SolutionParser_projects.GetValue(solutionParser, null);
            for (int i = 0; i < array.Length; i++)
            {
                var model = new SolutionProject(array.GetValue(i));
                model.AbsolutePath = Path.GetDirectoryName(solutionFileName)+"/"+ model.RelativePath;
                projects.Add(model);
            }
            this.Projects = projects;
            GetProjectFullName(solutionFileName);
            this.SolutionName = Path.GetFileName(solutionFileName);
        }

        private void GetProjectFullName(string solutionFileName)
        {
            DirectoryInfo solution = (new FileInfo(solutionFileName)).Directory;
            foreach (var temp in Projects.Where
                //(temp=>temp.RelativePath.EndsWith("csproj"))
                (temp => !temp.RelativePath.Equals(temp.ProjectName))
            )
            {
                GetProjectFullName(solution, temp);
            }
        }

        private void GetProjectFullName(DirectoryInfo solution, SolutionProject project)
        {
            //Uri newUri =new Uri(,UriKind./*Absolute*/);
            //if(project.RelativePath)

            project.FullName = System.IO.Path.Combine(solution.FullName, project.RelativePath);
        }


        //public string GetProjectPath(string match)
        //{
        //    var list = this.Projects.Where(m => m.FullName != null
        //                                        && match.Contains(Path.Combine(Path.GetDirectoryName(m.RelativePath), Path.GetFileNameWithoutExtension(m.RelativePath))))//拼装路径，例如把.csproj干掉 ，SERP3.0.Common\SERP3.0.Common.csproj
        //                                       .ToList();

        //    if (list.Count == 0)//路径匹配不到，匹配文件名
        //    {
        //        list = this.Projects.Where(m => m.FullName != null && match.Contains(m.ProjectName))//拼装路径，例如把.csproj干掉 ，SERP3.0.Common\SERP3.0.Common.csproj
        //                .ToList();
        //        if (list.Count == 0)
        //        {
        //            return "";
        //        }

        //    }
        //    string temp = "first";
        //    string rst = "";
        //    foreach (var item in list)
        //    {
        //        var pathWithoutExtension = Path.Combine(Path.GetDirectoryName(item.RelativePath), Path.GetFileNameWithoutExtension(item.RelativePath));
        //        var newPath = match.Replace(pathWithoutExtension, "");
        //        if (string.IsNullOrWhiteSpace(newPath))//完全匹配
        //        {
        //            return item.FullName;
        //        }
        //        if (temp == "first")//第一次进入
        //        {
        //            temp = newPath;
        //            rst = item.FullName;
        //        }
        //        else
        //        {
        //            if (temp.Length > newPath.Length)//字符串用replace替换，看哪个长度变最小了，那个就是最佳匹配
        //            {
        //                temp = newPath;
        //                rst = item.FullName;
        //            }
        //        }
        //    }
        //    return rst;

        //}

        public SolutionProject GetProjectPath(string match)
        {
            var stringCompute = new StringCompute();
            decimal rate = 0M;
            decimal tempRate = -1;
            SolutionProject sp = null;
            foreach (var item in this.Projects)
            {
                stringCompute.SpeedyCompute(match, item.RelativePath);
                tempRate = stringCompute.ComputeResult.Rate;
                  if (tempRate > rate)//选择相似度高的
                    {
                        rate = tempRate;
                        sp = item;
                    }                
            }
            return sp;
            
        }
    }
}
