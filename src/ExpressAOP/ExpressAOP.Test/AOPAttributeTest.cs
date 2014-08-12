using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExpressAOP.Test
{
    [TestClass]
    public class AOPAttributeTest
    {
        [TestMethod]
         public void BaseTest()
        {
            var list = new List<int>() {2, 3};
    
            Assert.AreEqual(new[]{2,3}.GetStr(),list.GetStr());
            Assert.AreNotEqual(new[] { 3, 2 }.GetStr(), list.GetStr());
        }
    }
}