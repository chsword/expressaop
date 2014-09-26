using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExpressAOP.Test
{
    [TestClass]
    public class TryCatchTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var t =new MyClass().Method();
            Console.WriteLine(t);
        }
    }
}