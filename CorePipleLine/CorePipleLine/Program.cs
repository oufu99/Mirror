using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CorePipleLine
{
    public delegate Task RequestDelegate(Context context);

    class Program
    {
        public static List<Func<RequestDelegate, RequestDelegate>> _list = new List<Func<RequestDelegate, RequestDelegate>>();

        public static Task Test1(Context context)
        {
            return null;
        }
        public static void Use(Func<RequestDelegate, RequestDelegate> middleWare)
        {
            _list.Add(middleWare);
        }
        static void Main(string[] args)
        {
            Use(next =>
            {
                return context =>
                {
                    Console.WriteLine(1);
                    return next.Invoke(context);
                };

            });

            #region 我的理解
            //入参Func<Delegate,Delegate>  外层返回void  但是Func入参Delegate,返回Delegate  然后Delegate的返回值是Task Task是Delegate.Invoke以后得到的数据
            //Use(c =>
            //{
            //    return x =>
            //    {
            //        return c.Invoke(x);
            //    };
            //}
            //); 
            #endregion

            Use(next =>
            {
                return context =>              //同上
                {
                    context.Write("2");
                    return next.Invoke(context);
                };
            });
            _list.Reverse();                        //把_list中的内容颠倒一下顺序，因为如果不颠倒下，后先执行最后加入的中间件，后执行最先加入中间件。
            RequestDelegate end = (context) =>
            {
                context.Write("end");
                return Task.CompletedTask;
            };
            foreach (var middleware in _list)
            {
                end = middleware.Invoke(end);     //把_list中的各个中间件“附加”到end委托上。
            }
            end.Invoke(new Context());           //调用end委托
            //_list[1].Invoke(end).Invoke(new Context());
            Console.ReadLine();

        }

        static void Main3(string[] args)
        {
            Task t = new Task(() =>
            {
                Console.WriteLine("123");
            });
            t = new Task(() =>
            {
                Console.WriteLine("432");
            });
            t.Start();
            Console.ReadLine();
        }

    }
}