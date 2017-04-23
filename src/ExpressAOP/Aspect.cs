using System;
using System.ComponentModel.Composition.Primitives;
using System.Diagnostics.Contracts;

namespace ExpressAOP
{
    internal class Aspect
	{
		public Aspect(IAOPFilter filter, Predicate<Export> pointcut)
		{
            Contract.Requires(filter != null, "Filter不能为空");
            Contract.Requires(pointcut != null, "切面不能为空");
			AopFilter = filter;
			Pointcut = pointcut;
		}

		public IAOPFilter AopFilter { get; private set; }

		public Predicate<Export> Pointcut { get; private set; }
	}
}