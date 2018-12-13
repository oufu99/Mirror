using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test
{
    /// <summary>
    /// OneModule 的摘要说明
    /// </summary>
    public class TwoModule : IHttpModule
    {
        public TwoModule()
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
            context.BeginRequest += context_BeginRequest;
        }
        private void context_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            app.Context.Response.Write("http://www.5566.net");
        }
    }
}