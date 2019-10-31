using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Aaron.HotUpdate
{
    /// <summary>
    /// 视图专用
    /// </summary>
    public class UrlStarupHelper
    {
        public static string DllName = "Aaron";
           
        public static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            AssemblyName assemblyName = new AssemblyName(args.Name);
            //判断此dll是我所写
            if (args.Name.Contains(DllName))
            {
                //用一个列表来缓存所有对象
                var list = AppDomain.CurrentDomain.GetData("AaronUpdateList") as List<string>;
                if (list == null)
                {
                    list = new List<string>();
                }
                if (!list.Contains(assemblyName.Name))
                {
                    list.Add(assemblyName.Name);
                    AppDomain.CurrentDomain.SetData("AaronUpdateList", list);
                }
            }
            var obj = AppDomain.CurrentDomain.GetData(assemblyName.Name);
            if (obj != null)
            {
                return (Assembly)obj;
            }

            var path = Path.Combine(AppContext.BaseDirectory, $"{assemblyName.Name}.dll");
            if (File.Exists(path))
            {
                byte[] data = File.ReadAllBytes(path);
                var assembly = Assembly.Load(data);
                AppDomain.CurrentDomain.SetData(assemblyName.Name, assembly);
                return assembly;
            }
            return null;
        }
        public static void InitStartup(IServiceCollection services)
        {
            services.AddMvc().ConfigureApplicationPartManager(manager =>
            {
                //移除ASP.NET CORE MVC管理器中默认内置的MetadataReferenceFeatureProvider，该Provider如果不移除，还是会引发InvalidOperationException: Cannot find compilation library location for package 'MyNetCoreLib'这个错误
                manager.FeatureProviders.Remove(manager.FeatureProviders.First(f => f is MetadataReferenceFeatureProvider));
                //注册我们定义的ReferencesMetadataReferenceFeatureProvider到ASP.NET CORE MVC管理器来代替上面移除的MetadataReferenceFeatureProvider
                manager.FeatureProviders.Add(new HotUpdateMetadataReferenceFeatureProvider());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);


            services.AddSingleton<IActionDescriptorChangeProvider>(HotUpdateActionDescriptorChangeProvider.Instance);
            services.AddSingleton(HotUpdateActionDescriptorChangeProvider.Instance);
            // 注册自定义视图缓存处理
            var razorViewEngine = services.FirstOrDefault(p => p.ServiceType == typeof(IRazorViewEngine));
            services.Remove(razorViewEngine);
            services.AddSingleton<IRazorViewEngine, HotUpdateRazorViewEngine>();
            var viewCompilerProvider = services.FirstOrDefault(p => p.ServiceType == typeof(IViewCompilerProvider));
            services.Remove(viewCompilerProvider);
            services.AddSingleton<IViewCompilerProvider, HotUpdateRazorViewCompilerProvider>();
        }
    }
}
