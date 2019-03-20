using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MVC.Models
{
    public class ActionTrackAttribute : ActionFilterAttribute
    {
        Stopwatch sw;
        StringBuilder message = new StringBuilder();


        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    sw = new Stopwatch();
        //    sw.Start();
        //    message.Append("页面被访问，Uri:" + filterContext.RequestContext.HttpContext.Request.Url.AbsoluteUri);
        //    //base.OnActionExecuting(filterContext);
        //}

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            sw = new Stopwatch();
            sw.Start();
            message.Append("页面被访问，Uri:" + filterContext.RequestContext.HttpContext.Request.Url.AbsoluteUri);
        }

    }
}