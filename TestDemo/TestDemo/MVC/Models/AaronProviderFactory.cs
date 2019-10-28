using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC 
{
    public class AaronProviderFactory : IModelBinderProvider
    {
        public IModelBinder GetBinder(Type modelType)
        {
            return new AaronBinder();
            //if (modelType.IsSubclassOf(typeof(Person)) || modelType== typeof(Person))
            //{
            //    return new AaronBinder(); 
            //}
            //return null;
        }
    }
}