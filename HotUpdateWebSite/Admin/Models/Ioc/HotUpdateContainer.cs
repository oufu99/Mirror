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
        private Dictionary<string, string> AssemblyDic { get; set; } = new Dictionary<string, string>();
        private List<HotUpdateServiceDescriptor> HotUpdateList { get; set; } = new List<HotUpdateServiceDescriptor>();

        public HotUpdateContainer()
        {
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="assemblyPath"></param>
        /// <param name="interfacesName">接口名称 用,分隔 用来在热更新的时候把他的类型清空好重新赋值</param>
        public void RegisterAssemblyPaths(string assemblyPath, string interfacesName)
        {
            AssemblyDic.Add(assemblyPath, interfacesName);
        }

        /// <summary>
        /// 修改逻辑
        /// </summary>
        /// <param name="model"></param>
        internal void Update(params string[] path)
        {
            Build(path, true);
        }

        internal Dictionary<string, string> GetAssemblyDic()
        {
            return AssemblyDic;
        }

        internal HotUpdateServiceDescriptor[] GetHotUpdateList()
        {
            return HotUpdateList.ToArray();
        }

        /// <summary>
        /// 加载dll的所有类
        /// </summary>[] 
        public void Build(string[] assemblyPaths = null, bool isUpdate = false)
        {
            var dicList = new List<string>();
            foreach (var item in AssemblyDic)
            {
                dicList.Add(item.Key);
            }
            assemblyPaths = assemblyPaths ?? dicList.ToArray();
            ////重新擦除所以IService
            //AppDomain.CurrentDomain.SetData("IService", null);
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

            var old = HotUpdateList.FirstOrDefault(c => c.AssemblyObj.GetName().Name == model.AssemblyObj.GetName().Name && c.ServiceType.Name == model.ServiceType.Name);
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
