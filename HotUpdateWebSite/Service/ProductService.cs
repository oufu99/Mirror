using IServices;
using System;

namespace Service
{
    public class ProductService : IProductService
    {
        public string Name { get; set; } = "Aaron";

        public string Introduce()
        {
            return Name;
        }

        public void Test()
        {
            System.IO.File.AppendAllText(@"d:\jialin.txt", "调用测试");
        }

        public string Test2()
        {
            return "我是心的";
        }
    }
}
