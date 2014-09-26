using System;

namespace ExpressAOP.Test
{
    [AOPProxy]
    class MyClass:ContextBoundObject
    {
        [TryCatchFilter]
        public int Method()
        {
            throw new Exception("error");
        }
    }
}