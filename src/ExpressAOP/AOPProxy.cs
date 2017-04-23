namespace ExpressAOP
{
    /// <summary>
    /// Aop������
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
        /// ��ȡ�����͵Ĵ�����
        /// </summary>
        /// <returns></returns>
        public T GetObject()
        {
            return (T) Proxy.GetTransparentProxy();
        }
    }
}