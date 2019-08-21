using Consul;
using Polly;
using RestTemplateCore;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading;

namespace UnitTest
{
    class Program
    {
        static void Main(string[] args)
        {
            #region RestTemplate代码
            //using (HttpClient httpClient = new HttpClient())
            //{
            //    RestTemplate rest = new RestTemplate(httpClient);
            //    Console.WriteLine("---查询数据---------");
            //    var ret1 =
            //    rest.GetForEntityAsync<Product[]>("http://ProductService/api/Product/").Result;
            //    Console.WriteLine(ret1.StatusCode);
            //    if (ret1.StatusCode == System.Net.HttpStatusCode.OK)
            //    {
            //        foreach (var p in ret1.Body)
            //        {
            //            Console.WriteLine($"id={p.Id},name={p.Name}");
            //        }
            //    }
            //    Console.WriteLine("---新增数据---------");
            //    Product newP = new Product();
            //    newP.Id = 888;
            //    newP.Name = "新增";
            //    newP.Price = 88.8;
            //    var ret = rest.PostAsync("http://ProductService/api/Product/", newP).Result;


            //    ret1 =
            //                  rest.GetForEntityAsync<Product[]>("http://ProductService/api/Product/").Result;
            //    if (ret1.StatusCode == System.Net.HttpStatusCode.OK)
            //    {
            //        foreach (var p in ret1.Body)
            //        {
            //            Console.WriteLine($"id={p.Id},name={p.Name}");
            //        }
            //    }

            //    Console.WriteLine(ret.StatusCode);
            //} 

            #endregion

            #region Polly
            //Polly 的策略由“故障”和“动作”两部分组成
            //故障 包括异常、超时、返回值错等情况
            //动作 包括 FallBack(降级)、重试（Retry）、熔断（Circuit - breaker）
            #region 降级

            //Policy policy1 = Policy
            //    .Handle<ArgumentException>() //故障
            //    .Fallback(() =>//动作
            //    {
            //        Console.WriteLine("降级1");
            //    }, ex =>
            //            {
            //                Console.WriteLine(ex.Message);
            //            });
            //Policy policy2 = Policy
            //    .Handle<ArgumentException>() //故障
            //    .Fallback(() =>//动作
            //    {
            //        Console.WriteLine("降级2");
            //    }, ex =>
            //    {
            //        Console.WriteLine(ex.Message);
            //    });
            //Policy policy3 = Policy
            //  .Handle<ArgumentException>() //故障
            //  .Fallback(() =>//动作
            //    {
            //        Console.WriteLine("降级3");
            //    }, ex =>
            //    {
            //        Console.WriteLine(ex.Message);
            //    });
            //var policy9 = Policy.Wrap(policy1, policy2, policy3);

            //policy9.Execute(() =>
            //{
            //    Console.WriteLine("开始任务");
            //    throw new ArgumentException("Hello world!");
            //    Console.WriteLine("完成任务");
            //});
            #endregion


            #region 重试  Retry(n) 重试N次
            //            Policy policy = Policy
            //.Handle<Exception>()
            //.Retry(3);
            //            policy.Execute(() =>
            //            {
            //                Console.WriteLine("开始任务");
            //                if (DateTime.Now.Second % 10 != 0)
            //                {
            //                    throw new Exception("出错");
            //                }
            //                Console.WriteLine("完成任务");
            //            });


            #endregion

            #region 熔断
            //Policy policy = Policy.Handle<Exception>().CircuitBreaker(6, TimeSpan.FromSeconds(5));//连续出错6次之后熔断5秒(不会再去尝试执行业务代码）。
            //while (true)
            //{
            //    Console.WriteLine("开始Execute");
            //    try
            //    {
            //        policy.Execute(() =>
            //        {
            //            Console.WriteLine("开始任务");
            //            throw new Exception("出错");
            //            Console.WriteLine("完成任务");
            //        });
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine("execute出错" + ex.Message);
            //    }
            //}
            #endregion

            #endregion

            double gs = 150;
            double jg = 1.5;
            var result = ((100 + gs) * 0.01) / jg;
            Console.WriteLine(result);

            var gs = (result * jg) * 100 - 100;
            //计算

            //0  0.58  100  1.17  200  1.76  300 2.35  400 2.94
            //0  0.66  100  1.5     400 3.33
            //计算出要达到那个每秒攻击次数需要的攻速

            Console.ReadLine();
        }
    }
    class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
    }
}
