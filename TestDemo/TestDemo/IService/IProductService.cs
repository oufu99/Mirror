using System;

namespace IService
{
    public interface IProductService
    {
        string Name { get; set; }
        string Introduce();
        void Test();
    }
}
