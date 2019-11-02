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

namespace Aaron.HotUpdate
{
    public class HotUpdateContainer
    {
<<<<<<< HEAD:HotUpdateWebSite/Admin/Models/Ioc/HotUpdateContainer.cs
        private List<string> AssemblyPath { get; set; } = new List<string>();
        private List<HotUpdateServiceDescriptor> HotUpdateList { get; set; } = new List<HotUpdateServiceDescriptor>();
        
=======
        public Dictionary<string, string> AssemblyDic { get; set; } = new Dictionary<string, string>();
        public List<HotUpdateServiceDescriptor> HotUpdateList { get; set; } = new List<HotUpdateServiceDescriptor>();

>>>>>>> 2be2ee204853a3a09080b4ca48e0c680fdc4c4f7:HotUpdateWebSite/Aaron.HotUpdate/HotupdateDll/HotUpdateContainer.cs
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
        public void Update(params string[] path)
        {
            Build(path);
        }
<<<<<<< HEAD:HotUpdateWebSite/Admin/Models/Ioc/HotUpdateContainer.cs
        
        internal HotUpdateServiceDescriptor[] GetHotUpdateList()
=======

        public Dictionary<string, string> GetAssemblyDic()
        {
            return AssemblyDic;
        }

        public HotUpdateServiceDescriptor[] GetHotUpdateList()
>>>>>>> 2be2ee204853a3a09080b4ca48e0c680fdc4c4f7:HotUpdateWebSite/Aaron.HotUpdate/HotupdateDll/HotUpdateContainer.cs
        {
            return HotUpdateList.ToArray();
        }

        /// <summary>
        /// 加载dll的所有类
        /// </summary>[] 
        public void Build(string[] assemblyPaths = null)
        {
<<<<<<< HEAD:HotUpdateWebSite/Admin/Models/Ioc/HotUpdateContainer.cs
            assemblyPaths = assemblyPaths ?? AssemblyPath.ToArray();
=======
            var dicList = new List<string>();
            foreach (var item in AssemblyDic)
            {
                dicList.Add(item.Key);
            }
            if (assemblyPaths == null || assemblyPaths.Length == 0)
            {
                assemblyPaths = dicList.ToArray();
            }
>>>>>>> 2be2ee204853a3a09080b4ca48e0c680fdc4c4f7:HotUpdateWebSite/Aaron.HotUpdate/HotupdateDll/HotUpdateContainer.cs
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

    public class HotUpdateServiceDescriptor
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

        /// <summary>
        /// 实现对象(目前只有IServiceProvider这个对象使用)
        /// </summary>
        public object ImplementationObject { get; set; }

    }
}
