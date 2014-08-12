namespace ExpressAOP
{
    /// <summary>
    /// Aop代理类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AOPProxy<T>
    {
        RealProxyWrapper Proxy { get; set; }

        public AOPProxy(object instance)
        {
            Proxy = new RealProxyWrapper(typeof(T), instance);
        }

        /// <summary>
        /// 获取此类型的代理类
        /// </summary>
        /// <returns></returns>
        public T GetObject()
        {
            return (T) Proxy.GetTransparentProxy();
        }
    }
}