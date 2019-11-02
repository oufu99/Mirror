using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
<<<<<<< HEAD
using Admin.Models;
=======
using Aaron.HotUpdate;
>>>>>>> 2be2ee204853a3a09080b4ca48e0c680fdc4c4f7
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Admin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
<<<<<<< HEAD
        }

=======
            AppDomain.CurrentDomain.AssemblyResolve += UrlStarupHelper.CurrentDomain_AssemblyResolve;
        }



>>>>>>> 2be2ee204853a3a09080b4ca48e0c680fdc4c4f7
        public IConfiguration Configuration { get; }



        /// <summary>
        /// 添加自己的服务和对象 到容器中去
        /// </summary>
        /// <param name="services"></param>
        //public IServiceProvider ConfigureServices(IServiceCollection services)
        public void ConfigureServices(IServiceCollection services)

        {
            UrlStarupHelper.InitStartup(services);

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc();
<<<<<<< HEAD
            //var hotUpdateContainer = new HotUpdateContainer();
            //var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
            //hotUpdateContainer.RegisterAssemblyPaths(Path.Combine(basePath, "Services.dll"));
            //return new HotUpdateServiceProvider(services, hotUpdateContainer);
=======
            var hotUpdateContainer = new HotUpdateContainer();            
            return new HotUpdateServiceProvider(services, hotUpdateContainer);
>>>>>>> 2be2ee204853a3a09080b4ca48e0c680fdc4c4f7
        }

        /// <summary>
        /// 添加管道事件  中间件
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "HotUpdate", action = "Index" }
                );
            });
        }
    }
}
