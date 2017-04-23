
using System;
using System.Runtime.Remoting.Messaging;

namespace ExpressAOP
{
    public interface IProcesser
	{
        /// <summary>
        /// 调用目标类型
        /// </summary>
		Type Type { get; }

        /// <summary>
        /// 调用目标实例
        /// </summary>
		Object Instance { get; }

        /// <summary>
        /// 方法调用时使用的消息
        /// </summary>
		IMethodCallMessage MethodCallMessage { get; }

        /// <summary>
        /// 方法返回时使用的消息
        /// </summary>
		IMethodReturnMessage MethodReturnMessage { get; }

        /// <summary>
        /// 方法执行事件
        /// </summary>
		void Process();

        /// <summary>
        /// 附加Return值 
        /// </summary>
        /// <param name="message"></param>
        void AttachReturnMessage(IMethodReturnMessage message);
	}
}