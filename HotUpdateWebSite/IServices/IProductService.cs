using System;

namespace IServices
{
    public interface IProductService
    {
        string Name { get; set; }
        string Introduce();
        void Test();
    }
}
