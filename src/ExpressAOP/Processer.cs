using System;
using System.Runtime.Remoting.Messaging;

namespace ExpressAOP
{
    internal class Processer : IProcesser
	{
		public Type Type { get; private set; }

		public Object Instance { get; private set; }

		public IMethodCallMessage MethodCallMessage { get; private set; }

		public IMethodReturnMessage MethodReturnMessage { get; private set; }

		public Processer(Type targetType, object targetInstance, IMethodCallMessage methodCallMessage)
		{
			Type = targetType;
			Instance = targetInstance;
			MethodCallMessage = methodCallMessage;
		}

		public void Process()
		{
			var method = MethodCallMessage.MethodBase;
			var ret = method.Invoke(Instance, MethodCallMessage.Args);
			MethodReturnMessage = new ReturnMessage(
				ret, MethodCallMessage.Args, MethodCallMessage.ArgCount, MethodCallMessage.LogicalCallContext, MethodCallMessage);
		}

        public void AttachReturnMessage(IMethodReturnMessage message)
        {
            MethodReturnMessage = message;
        }
	}
}
