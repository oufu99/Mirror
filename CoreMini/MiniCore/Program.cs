using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniCore
{
    public delegate Task RequestDelegate(Context context);
    class Program
    {
        static List<Func<RequestDelegate, RequestDelegate>> list = new List<Func<RequestDelegate, RequestDelegate>>();

        public static void Use(Func<RequestDelegate, RequestDelegate> middleWare)
        {
            list.Add(middleWare);
        }

        public static void Main()
        {
            Console.WriteLine("gogogo");




            Use(next =>
        {
            return context =>
            {
                context.Write("11");
                return next.Invoke(context);
            };
        });
            Use(next =>
            {
                return context =>
                {
                    context.Write("22");
                    return next.Invoke(context);
                };
            });
            Use(next =>
            {
                return context =>
                {
                    context.Write("33");
                    return next.Invoke(context);
                };
            });
            list.Reverse();
            RequestDelegate end = (context) =>
            {
                context.Write("end");
                return Task.CompletedTask;
            };

            foreach (var item in list)
            {
                end = item.Invoke(end);
            };
            end.Invoke(new Context());
            Console.ReadLine();
        }
    }
    public class Context
    {
        public string Request { get; set; }
        public string Response { get; set; }
        public void Write(string str)
        {
            this.Response += str;
        }
    }
}


