using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Admin.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace Admin.Controllers
{
    public class HotUpdateController : Controller
    {
        private readonly ApplicationPartManager _partManager;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly HotUpdateActionDescriptorChangeProvider _HotUpdateActionDescriptorChangeProvider;
        public HotUpdateController(
            ApplicationPartManager partManager,
            IHostingEnvironment env, HotUpdateActionDescriptorChangeProvider hotUpdateActionDescriptorChangeProvider)
        {
            _partManager = partManager;
            _hostingEnvironment = env;
            _HotUpdateActionDescriptorChangeProvider = hotUpdateActionDescriptorChangeProvider;
        }
        public IActionResult Index()
        {
            var list = new List<string>();
            list.Add("NewWeb");
            foreach (var dllName in list)
            {
                var fullPath = AppContext.BaseDirectory + dllName + ".dll";
                byte[] data1 = System.IO.File.ReadAllBytes(fullPath);

                var assembly = Assembly.Load(data1);

                var dllList = _partManager.ApplicationParts.Where(c => c.Name.Contains(dllName)).ToList();
                if (dllList.Count() > 0)
                {
                    foreach (var item in dllList)
                    {
                        _partManager.ApplicationParts.Remove(item);
                    }
                }
                //把对应的反射重新添加到内存中
                _partManager.ApplicationParts.Add(new AssemblyPart(assembly));
            }

            //通知系统,ActionProvider有更新
            _HotUpdateActionDescriptorChangeProvider.HasChanged = true;
            _HotUpdateActionDescriptorChangeProvider.TokenSource.Cancel();

            return Content("haha");
        }

        /// <summary>
        /// 热更新dll 如Service等
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string UpdateDLL()
        {
            var container = (HttpContext.RequestServices as HotUpdateServiceProvider).Container;
            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
            basePath = Path.Combine(basePath, "Services.dll");

            //反射获取父类



            //assemblyPaths = assemblyPaths ?? AssemblyPath.ToArray();
            //foreach (var assemblyPath in assemblyPaths)
            //{
            //    byte[] bt = File.ReadAllBytes(assemblyPath);
            //    var assembly = Assembly.Load(bt);
            //    var types = assembly.GetTypes();
            //    foreach (var objType in types)
            //    {
            //        var interfaces = objType.GetInterfaces();
            //        foreach (var item in interfaces)
            //        {
            //            //重新擦除所以IService
            //            if (isUpdate)
            //            {
            //                //AppDomain.CurrentDomain.SetData(item.Assembly.GetName().Name, null);
            //            }

            //            HotUpdateServiceDescriptor model = new HotUpdateServiceDescriptor();
            //            model.ImplementationType = objType;
            //            model.ServiceType = item;
            //            model.AssemblyObj = assembly;
            //            model.AssemblyPath = assemblyPath;
            //            CheckExistAndInsert(model);
            //        }
            //    }
            //}
            //
            AppDomain.CurrentDomain.SetData("IServices", null);
            container.Update(basePath);
            return "ok";
        }
    }
}