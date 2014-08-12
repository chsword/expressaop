using System;
using System.Runtime.Remoting.Proxies;
using System.Security.Permissions;

namespace ExpressAOP
{
    [AttributeUsage(AttributeTargets.Class)]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.Infrastructure)]

    public class AOPProxyAttribute : ProxyAttribute
    {
        public override MarshalByRefObject CreateInstance(Type serverType)
        {
            MarshalByRefObject obj = base.CreateInstance(serverType);
            var proxy = new RealProxyWrapper(serverType, obj);
            return (MarshalByRefObject)proxy.GetTransparentProxy();
        }
    }
}