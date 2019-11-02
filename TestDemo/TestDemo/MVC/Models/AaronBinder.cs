using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MVC
{
    public class AaronBinder : DefaultModelBinder, IModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {            
            var req = controllerContext.HttpContext.Request;

            //var stream = req.InputStream;
            //stream.Position = 0;
            //StreamReader reader = new StreamReader(stream);
            //string text = reader.ReadToEnd();
            //if (bindingContext.)
            //{

            //}

            string msgBody = string.Empty;
            using (var ms = new MemoryStream())
            {

                req.InputStream.CopyTo(ms);
                msgBody = System.Text.Encoding.UTF8.GetString(ms.ToArray());
            }
            
            var model = base.BindModel(controllerContext, bindingContext);
            //模型绑定
            Person p = model as Person;
            if (p == null)
            {
                return model;
            }
            //如果已经有传值,就不获取
            if (p.MySon == null)
            {
                Son s = new Son() { Name = "binder" };
                p.MySon = s; ;
            }

            return p;
        }



    }
}