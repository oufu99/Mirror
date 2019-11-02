using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Aaron.HotUpdate;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;

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
            //放需要热更新的Dll名称
            return View();
        }

        /// <summary>
        /// 热更新dll 如Service等
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string UpdateDLL([FromBody]List<string> list)
        {

            var container = (HttpContext.RequestServices as HotUpdateServiceProvider).Container;
            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;

            //拿到容器维护的那个字典对象
            var dic = container.GetAssemblyDic();
            foreach (var item in list)
            {
                var dllPath = Path.Combine(basePath, item + ".dll");
                if (!dic.ContainsKey(dllPath))
                {

                    //把映射关系加入容器  就把新的映射关系存进去 先把dll去掉 好拿到要加I的地方
                    var index = item.Replace(".dll", "").LastIndexOf(".");
                    var interfaceName = item.Insert(index + 1, "I") + ".dll";
                    //需要添加后缀,后面直接根据此dll名称反射
                    container.RegisterAssemblyPaths(dllPath, interfaceName);

                }
                var interfaces = dic[dllPath];
                var interfaceNames = interfaces.Split(new string[] { @"," }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var Iitem in interfaceNames)
                {
                    //防止更新IService和原程序不一样 直接删除当前程序域的IService让系统重新获取
                    AppDomain.CurrentDomain.SetData(Iitem, null);
                }
            }

            //把这些参数重新存入到内存中
            container.Update();
            return "ok";
        }

        /// <summary>
        /// 热更新方法
        /// </summary>
        /// <returns></returns>
        public IActionResult UpdateAdmin([FromBody]List<string> list)
        {
            //清空dll缓存
            var dllRemoveList = AppDomain.CurrentDomain.GetData(UrlStarupHelper.DllName) as List<string>;
            if (dllRemoveList != null)
            {
                foreach (var item in dllRemoveList)
                {
                    AppDomain.CurrentDomain.SetData(item, null);
                }
            }

            // 清空视图缓存
            var razorViewEngine = HttpContext.RequestServices.GetService(typeof(IRazorViewEngine)) as HotUpdateRazorViewEngine;
            var viewCompilerProvider = HttpContext.RequestServices.GetService(typeof(IViewCompilerProvider)) as HotUpdateRazorViewCompilerProvider;
            var viewCompiler = viewCompilerProvider.GetCompiler() as HotUpdateRazorViewCompiler;
            razorViewEngine.ClearViewCache();
            viewCompiler.ClearViewCache();

            foreach (var dllName in list)
            {
                var fullPath = AppContext.BaseDirectory + dllName + ".dll";
                if (!System.IO.File.Exists(fullPath))
                    throw new ApplicationException($"not find {dllName}");

                byte[] data1 = System.IO.File.ReadAllBytes(fullPath);
                //var ms = new MemoryStream(data1);

                var assembly = Assembly.Load(data1);

                var dllList = _partManager.ApplicationParts.Where(c => c.Name == dllName).ToList();
                if (dllList.Count() > 0)
                {
                    foreach (var item in dllList)
                    {
                        _partManager.ApplicationParts.Remove(item);
                    }
                }

                if (dllName.LastIndexOf(".PrecompiledViews") > 0)
                    _partManager.ApplicationParts.Add(new CompiledRazorAssemblyPart(assembly));
                else
                    _partManager.ApplicationParts.Add(new AssemblyPart(assembly));
            }

            _HotUpdateActionDescriptorChangeProvider.HasChanged = true;
            _HotUpdateActionDescriptorChangeProvider.TokenSource.Cancel();
            return Content("成功");

<<<<<<< HEAD
            return Content("haha");
=======
        }

        [HttpPost]
        public string TestDll(string[] arr)
        {
            return "ok";
>>>>>>> 2be2ee204853a3a09080b4ca48e0c680fdc4c4f7
        }
    }
}