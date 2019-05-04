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
            var actionName = filterContext.RouteData.Values["action"].ToString();
            var areaName = (filterContext.RouteData.DataTokens["area"] == null ? "" : filterContext.RouteData.DataTokens["area"]).ToString();


            #region 判断是否需要跳过登录验证
            //如果方法贴上IgnoreFilter
            Assembly assembly = null;
            if (string.IsNullOrEmpty(areaName))
            {
                //获取当前程序集 
                assembly = Assembly.GetExecutingAssembly(); 
            }
            else
            {
                //把bin目录中对应的dll进行反射
                string assPath = $@"{AppDomain.CurrentDomain.BaseDirectory}\bin\{ConfigHelper.GetAppConfig("baseNameSpace")}.{areaName}.dll";
                assembly = Assembly.LoadFrom(assPath);
            }
            //获取反射的程序集的名称  以后定制 也要符合 Aaron.Youzuan 这种标准
            var nameSpace = assembly.GetName().Name;
            Type type = assembly.GetType($"{nameSpace}.Controllers.{controllerName}Controller");
            //判断这个类贴上了跳过标签没有 
            var classAttr = type.GetCustomAttribute(typeof(IgnoreFilter));
            if (classAttr != null)
            {
                return;
            }

            var methods = type.GetMethods().Where(c => c.Name == actionName);
            //获取请求方式
            var isPost = HttpContext.Current.Request.RequestType == "POST";
            bool ignore = false;
            MethodInfo method = null;
            //可能有方法会重载  所以遍历所有同名方法
            foreach (var item in methods)
            {
                var postAttrs = item.GetCustomAttributes(false);
                var isOnly = postAttrs.Where(c => c is HttpPostAttribute || c is HttpGetAttribute);
                //如果没有打Get和Post的标签就说明他通用,通用就不管了,直接把这个赋值给他
                if (isOnly.Count() == 0)
                {
                    method = item;
                    break;
                }
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
            if (ignore)
            {
                return;
            }
            #endregion

            //通过IsLogin来判断是否登录  就算被伪造,进来没有jwt字符串也没用
            string isLogin = CookieHelper.GetCookie("IsLogin");
            if (string.IsNullOrEmpty(isLogin) || isLogin != "1")
            {
                string loginPath = "/Login/Index";
                //添加当路径用来作为登录后跳转的位置
                loginPath += string.Format("?returnUrl={0}", filterContext.HttpContext.Request.RawUrl.ToString());
                filterContext.Result = new RedirectResult(loginPath);
            }
        }
    }
}