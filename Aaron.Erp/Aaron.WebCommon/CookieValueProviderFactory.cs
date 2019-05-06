using Aaron.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Aaron.WebCommon
{
    public class CookieValueProviderFactory : IModelBinderProvider
    {
        public IModelBinder GetBinder(Type modelType)
        {
            return new CookieBinder();
            ////判断是否是BaseType的子类
            //if (modelType.IsSubclassOf(typeof(BaseModel)))
            //{
            //    return new CookieBinder();
            //}

            //return null;
        }
    }
}
