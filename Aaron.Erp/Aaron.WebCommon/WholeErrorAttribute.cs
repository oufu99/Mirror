using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Aaron.WebCommon
{
    public class WholeErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);

            //错误处理  记录日志等
            System.IO.File.AppendAllText(@"d:\jialin.txt", "\n\r全局错误如下:");
            System.IO.File.AppendAllText(@"d:\jialin.txt", filterContext.Exception.Message);
            //设置为true阻止golbal里面的错误执行
            filterContext.ExceptionHandled = true;

            var res = new ResponseModel();
            res.Code = 500;
            res.Msg = filterContext.Exception.Message;
            //如果是ajax请求就直接返回一个对象  普通请求就跳转到错误页面
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.HttpContext.Response.ContentType = "application/json";
                filterContext.HttpContext.Response.ContentEncoding = Encoding.UTF8;
                filterContext.HttpContext.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(res));
                filterContext.HttpContext.Response.End();
            }
            else
            {
                //把需要传的参数用字典类型传过去
                object obj = res.Msg;
                var dic = new ViewDataDictionary();
                dic.Add("Msg", obj);

                filterContext.Result = new ViewResult() { ViewName = "/Views/Error/Index.cshtml", ViewData = dic };
            }
        }
    }
}
