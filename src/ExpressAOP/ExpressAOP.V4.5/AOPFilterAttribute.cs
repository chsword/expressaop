namespace ExpressAOP
{
    /// <summary>
    /// 拥有前置与后置事件的AopFilter
    /// </summary>
    public abstract class AOPFilterAttribute : AOPAttribute
    {
        public override void Execute(IProcesser processer)
        {
            Executing(processer);
            processer.Process();
            Executed(processer);
        }

        /// <summary>
        /// 前置事件
        /// </summary>
        /// <param name="processer"></param>
        protected virtual void Executing(IProcesser processer) { }

        /// <summary>
        /// 后置事件
        /// </summary>
        /// <param name="processer"></param>
        protected virtual void Executed(IProcesser processer) { }
    }
}