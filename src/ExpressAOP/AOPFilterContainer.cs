using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;

namespace ExpressAOP
{

    internal class AOPFilterContainer : IAOPFilter
    {
        private readonly ReadOnlyCollection<IAOPFilter> _filters;

        public AOPFilterContainer(IEnumerable<IAOPFilter> filters)
        {
            Contract.Requires(null != filters, "filters参数不能为空");
            _filters = filters.ToReadOnlyCollection();
        }

        public void Execute(IProcesser processer)
        {
            using (var enumerator = _filters.GetEnumerator())
            {
                var wrapper = new ProcesserWrapper(processer, enumerator);
                wrapper.Process();
            }
        }

    
    }
}