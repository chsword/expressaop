using System;

namespace ExpressAOP.Test
{
    [AOPProxy]
    class MyModel2 : ContextBoundObject
    {
        [MyFilter(":filter")]
        public void Foo1()
        {
            Setting.List.Add("method");
        }
    }
}