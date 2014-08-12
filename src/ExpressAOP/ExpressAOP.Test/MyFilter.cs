using System;

namespace ExpressAOP.Test
{
    public class MyFilter : AOPFilterAttribute
    {
        private readonly string _key;

        public MyFilter(String key)
        {
            _key = key;
        }
        protected override void Executing(IProcesser processer)
        {
            Setting.List.Add("Executing-" + _key);
        }
        protected override void Executed(IProcesser processer)
        {
            Setting.List.Add("Executed-" + _key);
        }
    }
}