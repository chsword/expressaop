namespace ExpressAOP
{
    /// <summary>
    /// AopFilter接口
    /// </summary>
    public interface IAOPFilter
	{
        /// <summary>
        /// 执行方法时触发的方法
        /// </summary>
        /// <param name="processer"></param>
        void Execute(IProcesser processer);
	}
}
