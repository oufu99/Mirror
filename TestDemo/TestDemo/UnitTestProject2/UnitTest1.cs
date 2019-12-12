using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SendLib;

namespace UnitTestProject2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ResvAlipayTradeNo test = new ResvAlipayTradeNo();
            test.ResvId = "test_ResvId";
            test.AlipayTradeNo = "test_AlipayTradeNo";
            TestSend.MyTest(test);

        }
    }
}
