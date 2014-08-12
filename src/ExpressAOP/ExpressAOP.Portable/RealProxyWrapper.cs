using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Diagnostics.Contracts;
using System.Runtime.Remoting.Services;

namespace ExpressAOP
{
    /// <summary>
    /// RealProxy 包装器
    /// http://msdn.microsoft.com/zh-cn/library/system.runtime.remoting.proxies.realproxy.aspx
    /// </summary>
    internal class RealProxyWrapper : RealProxy
    {
        private readonly Type _interfaceType;

        private readonly object _instance;

        internal RealProxyWrapper(Type interfaceType, object instance)
            : base(interfaceType)
        {
            Contract.Requires(interfaceType != null, "interfaceType");
            Contract.Requires(instance != null, "instance");
            _interfaceType = interfaceType;
            _instance = instance;

        }

        /// <summary>
        /// 执行代理方法
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public override IMessage Invoke(IMessage message)
        {
        
            var callMessage = message as IMethodCallMessage;
            var constructionCallMessage = callMessage as IConstructionCallMessage;
            if (constructionCallMessage != null)
            {
                //if ctor
                var defaultProxy = RemotingServices.GetRealProxy(_instance);
                defaultProxy.InitializeServerObject(constructionCallMessage);
                return EnterpriseServicesHelper.CreateConstructionReturnMessage(constructionCallMessage,
                                                                                (MarshalByRefObject)
                                                                                GetTransparentProxy());
            }
            var processer = new Processer(_interfaceType, _instance, callMessage);
            if (callMessage != null)
            {
                #region Get Attributes

                //获取类方法上的Attributes
                var filters = GetFilterAttributes(callMessage.MethodName, callMessage.MethodSignature).ToList();

                //获取类上的
                var clsAttrs = _instance.GetType().GetCustomAttributes(typeof (IAOPFilter), false).Cast<IAOPFilter>();
                if (_interfaceType != null && _instance.GetType() != _interfaceType)
                {
                    //获取接口方法上的Attributes
                    var interfaceAttributes = callMessage.MethodBase.GetCustomAttributes(typeof (IAOPFilter), false)
                        .Cast<IAOPFilter>();
                    //获取接口上的
                    var intAttrs = _interfaceType.GetCustomAttributes(typeof (IAOPFilter), false).Cast<IAOPFilter>();
                    filters.InsertRange(0, interfaceAttributes);
                    filters.InsertRange(0, intAttrs);
                }
                filters.InsertRange(0, clsAttrs);
                #endregion

                new AOPFilterContainer(filters).Execute(processer);
            }
            return processer.MethodReturnMessage;
        }

        /// <summary>
        /// 获取Filter，顺序：接口的定义上》实例的类定义上》接口的方法上》实例的方法上
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="paramTypes"></param>
        /// <returns></returns>
        IEnumerable<IAOPFilter> GetFilterAttributes(string methodName, object paramTypes)
        {
            var instanceType = _instance.GetType();
            var memInfo = instanceType.GetMethod(methodName, (Type[])paramTypes);
            var attrs = memInfo.GetCustomAttributes(typeof(IAOPFilter), false);
            return attrs.Cast<IAOPFilter>();
        }
    }
}
