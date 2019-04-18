using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Models
{
    public class JwtBinder : DefaultModelBinder, IModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var model = base.BindModel(controllerContext, bindingContext);
            //模型绑定
            BaseModel baseModel = model as BaseModel;
            if (baseModel == null)
            {
                return model;
            }
            //从jwt字符串中解密后赋值

            return baseModel;
        }
    }
}
