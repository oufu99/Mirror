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
            var dllPath = Path.Combine(basePath, "Services.dll");

            //拿到容器维护的那个字典对象
            var dic = container.GetAssemblyDic();
            var interfaces = dic[dllPath];
            var interfaceNames = interfaces.Split(new string[] { @"," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in interfaceNames)
            {
                //防止更新IService和原程序不一样 直接删除当前程序域的IService让系统重新获取
                AppDomain.CurrentDomain.SetData(item, null);
            }
            container.Update(dllPath);
            return "ok";
        }

        [HttpPost]
        public string TestDll(string[] arr)
        {
            return "ok";
        }
    }
}