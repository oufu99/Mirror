using Aaron.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Aaron.Erp.App_Start.Filter
{
    public class LoginFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            var controllerName = (filterContext.RouteData.Values["controller"]).ToString();
            var actionName = (filterContext.RouteData.Values["action"]).ToString();
            var areaName = (filterContext.RouteData.DataTokens["area"] == null ? "" : filterContext.RouteData.DataTokens["area"]).ToString();

            //如果方法贴上IgnoreFilter
            Assembly assembly = null;
            if (string.IsNullOrEmpty(areaName))
            {
                assembly = Assembly.GetExecutingAssembly(); // 获取当前程序集 
            }
            else
            {
                assembly = Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + "\\bin\\" + "Aaron." + areaName + ".dll");
            }
            var name = assembly.GetName().Name;
            Type type = assembly.GetType($"{name}.Controllers.{controllerName}Controller");
            var methods = type.GetMethods().Where(c => c.Name == actionName);
            //获取请求方式
            var isPost = HttpContext.Current.Request.RequestType == "POST";
            bool ignore = false;
            MethodInfo method = null;
            //可能有方法会重载  所以遍历所有同名方法
            foreach (var item in methods)
            {
                var postAttrs = item.GetCustomAttributes(false);
                foreach (var attr in postAttrs)
                {
                    if (attr is HttpPostAttribute && isPost)
                    {
                        method = item;
                        break;
                    }
                    if (attr is HttpGetAttribute && !isPost)
                    {
                        method = item;
                        break;
                    }
                }
            }
            if (method != null)
            {
                var attrs = method.GetCustomAttributes(false);
                foreach (var item in attrs)
                {
                    if (item is IgnoreFilter)
                    {
                        ignore = true;
                        break;
                    }
                }
            }

            //通过IsLogin来判断是否登录  就算被伪造,进来没有jwt字符串也没用
            string isLogin = CookieHelper.GetCookie("IsLogin");
            if (!ignore || string.IsNullOrEmpty(isLogin) || isLogin != "1")
            {
                string loginPath = "/Login/Index";
                //添加当路径
                loginPath += string.Format("?returnUrl={0}", filterContext.HttpContext.Request.RawUrl.ToString());
                filterContext.Result = new RedirectResult(loginPath);
            }
        }
    }
}