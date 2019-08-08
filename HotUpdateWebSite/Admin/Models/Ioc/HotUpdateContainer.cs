using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Admin.Models
{
    public class HotUpdateContainer
    {
        private List<string> AssemblyPath { get; set; } = new List<string>();
        private List<HotUpdateServiceDescriptor> HotUpdateList { get; set; } = new List<HotUpdateServiceDescriptor>();
        
        public HotUpdateContainer()
        {
        }

        public void RegisterAssemblyPaths(string assemblyPath)
        {
            AssemblyPath.Add(assemblyPath);
        }

        /// <summary>
        /// 修改逻辑
        /// </summary>
        /// <param name="model"></param>
        internal void Update(params string[] path)
        {
            Build(path);
        }
        
        internal HotUpdateServiceDescriptor[] GetHotUpdateList()
        {
            return HotUpdateList.ToArray();
        }

        /// <summary>
        /// 加载dll的所有类
        /// </summary>[] 
        public void Build(string[] assemblyPaths = null)
        {
            assemblyPaths = assemblyPaths ?? AssemblyPath.ToArray();
            foreach (var assemblyPath in assemblyPaths)
            {
                byte[] bt = File.ReadAllBytes(assemblyPath);
                var assembly = Assembly.Load(bt);
                var types = assembly.GetTypes();
                foreach (var objType in types)
                {
                    var interfaces = objType.GetInterfaces();
                    foreach (var item in interfaces)
                    {
                        HotUpdateServiceDescriptor model = new HotUpdateServiceDescriptor();
                        model.ImplementationType = objType;
                        model.ServiceType = item;
                        model.AssemblyObj = assembly;
                        model.AssemblyPath = assemblyPath;
                        CheckExistAndInsert(model);
                    }
                }
            }

        }
        private void CheckExistAndInsert(HotUpdateServiceDescriptor model)
        {

            var old = HotUpdateList.FirstOrDefault(c => c.AssemblyObj.GetName().Name == model.AssemblyObj.GetName().Name &&  c.ServiceType.Name == model.ServiceType.Name);
            if (old != null)
            {
                HotUpdateList.Remove(old);
                HotUpdateList.Add(model);
                //old = ;
            }
            else
            {
                HotUpdateList.Add(model);
            }
        }

    }

    internal class HotUpdateServiceDescriptor
    {
        /// <summary>
        /// 实现类的Type
        /// </summary>
        public Type ImplementationType { get; set; }
        /// <summary>
        /// 接口的Type
        /// </summary>
        public Type ServiceType { get; set; }
        /// <summary>
        /// 实例  在build的时候实例化
        /// </summary>
        //public object ImplementationInstance { get; set; }
        /// <summary>
        /// 反射路径
        /// </summary>
        public string AssemblyPath { get; set; }
        /// <summary>
        ///  assembly对象
        /// </summary>
        public Assembly AssemblyObj { get; set; }

    }
}
