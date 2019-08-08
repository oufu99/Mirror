using IService;
using System;

namespace AaronModel
{
    public class ProductService : IProductService
    {
        public string Name { get; set; }

        public string Introduce()
        {
            return Name;
        }

        public void Test()
        {
            System.IO.File.AppendAllText(@"d:\jialin.txt", "翻转测试");
        }

    }
}
