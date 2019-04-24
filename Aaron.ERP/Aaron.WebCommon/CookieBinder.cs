using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aaron.WebCommon
{
    public class CookieBinder : DefaultModelBinder, IModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var model = base.BindModel(controllerContext, bindingContext);
            var header = HttpContext.Current.Request.Headers["Authorization"];
            //从请求头获取jwt字符串
            if (!string.IsNullOrEmpty(header))
            {
                return new BaseModel() { Name = "Aaron" };
            }
            return null;
        }
    }
}