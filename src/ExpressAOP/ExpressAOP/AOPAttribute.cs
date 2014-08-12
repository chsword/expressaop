using System;

namespace ExpressAOP
{
    /// <summary>
    /// AopAttribute�Ļ���
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = true)] 
    public abstract class AOPAttribute : Attribute, IAOPFilter
    {
        public virtual void Execute(IProcesser processer)
        {
            processer.Process();
        }
    }
}