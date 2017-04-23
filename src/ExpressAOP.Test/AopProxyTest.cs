using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExpressAOP.Test
{
    /// <summary>
    ///这是 AopProxyTest 的测试类，旨在
    ///包含所有 AopProxyTest 单元测试
    ///</summary>
    [TestClass]
    public class AopProxyTest
    {

        [TestCleanup]
        public void Cleanup()
        {
            Setting.List.Clear();
        }

        [TestMethod]
        public void FilterExecuteOrder()
        {
            var proxy = new AOPProxy<IMyModel>(new MyModel("1"));
            var obj = proxy.GetObject();
            obj.Foo(1);
            Assert.AreEqual(
                new[]
                    {

                        "Executing-I:filter",
                        "Executing-class:filter",
                        "Executing-I-Method1:filter",
                        "Executing-I-Method2:filter",
                        "Executing-method:filter",
                        "method-foo-1",
                        "Executed-method:filter",
                        "Executed-I-Method2:filter",
                        "Executed-I-Method1:filter",
                        "Executed-class:filter",
                        "Executed-I:filter",

                    }.GetStr()
                , Setting.List.GetStr());
        }
        [TestMethod]
        public void FilterExecuteOrderInProperty()
        {    
            var proxy = new AOPProxy<IMyModel>(new MyModel("1"));
            var hoge = proxy.GetObject();
            var t = hoge.MyProperty;
            Assert.AreEqual("1", t);
            Assert.AreEqual(new[]
                                { "Executing-I:filter", 
                                    "Executing-class:filter", 
                                   
                                    "Executing-I-prop-get:filter",
                                    "Executing-prop-get:filter", 
                                    "prop-ins-1",
                                    "Executed-prop-get:filter",
                                    "Executed-I-prop-get:filter", 
                                   
                                    "Executed-class:filter",
                                     "Executed-I:filter", 
                                }.GetStr(), Setting.List.GetStr());
        }
        [TestMethod]
        public void ClassContextBoundObject()
        {
            var o = new MyModel2();
            o.Foo1();
            Assert.AreEqual(new[]
                                {
                                    "Executing-:filter",
                                    "method",
                                    "Executed-:filter"
                                }.GetStr(), Setting.List.GetStr());
        }
       
      
       
    }
}
