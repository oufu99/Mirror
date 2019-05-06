using Aaron.Erp.Models;
using Aaron.Service;
using Aaron.WebCommon;
using IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Unity;
using Unity.Mvc5;

namespace Aaron.Erp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //模型绑定
            //ModelBinderProviders.BinderProviders.Add(new CookieValueProviderFactory());

            //Unity初始化
            BootStrapper.Initialise();
        }
    }
}
