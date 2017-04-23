using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.Remoting.Messaging;

namespace ExpressAOP
{
    internal class ProcesserWrapper : IProcesser
    {
        private readonly IProcesser _processer;

        private readonly IEnumerator<IAOPFilter> _filters;

        public ProcesserWrapper(IProcesser p, IEnumerator<IAOPFilter> filters)
        {
            Contract.Requires(null != p, "invocation");
            Contract.Requires(null != filters, "interceptors");
            _processer = p;
            _filters = filters;
        }

        public Type Type { get { return _processer.Type; } }

        public object Instance { get { return _processer.Instance; } }

        public IMethodCallMessage MethodCallMessage { get { return _processer.MethodCallMessage; } }

        public IMethodReturnMessage MethodReturnMessage { get { return _processer.MethodReturnMessage; } }

        public void Process()
        {
            if (_filters.MoveNext())
            {
                _filters.Current.Execute(this);
            }
            else
            {
                _processer.Process();
            }
        }

        public void AttachReturnMessage(IMethodReturnMessage message)
        {
            _processer.AttachReturnMessage(message);
        }
    }
}