using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Test
{
    /// <summary>
    /// OneModule 的摘要说明
    /// </summary>
    public class OneModule : IHttpModule
    {
        public OneModule()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        public void Dispose()
        {

        }

        public void Init(HttpApplication context)
        {
            context.EndRequest += context_BeginRequest;
        }
        private void context_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            app.Response.Write("结束请求");

        }
    }
}