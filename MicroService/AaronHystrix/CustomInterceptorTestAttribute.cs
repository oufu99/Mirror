using AspectCore.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hystrix
{
    public class CustomInterceptorTestAttribute : AbstractInterceptorAttribute
    {
        //每个被拦截的方法中执行
        public async override Task Invoke(AspectContext context, AspectDelegate next)
        {
            try
            {
                Console.WriteLine("执行代码前");
                await next(context);//执行被拦截的方法
            }
            catch (Exception)
            {
                Console.WriteLine("Service threw an exception!");
                throw;
            }
            finally
            {
                Console.WriteLine("执行完毕");
            }
        }
    }
}
