
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Diagnostics.Contracts;
using System.ComponentModel.Composition.Hosting;

namespace ExpressAOP
{
    internal class AOPContainer : CompositionContainer
	{
		protected ReadOnlyCollection<Aspect> Aspects { get; private set; }

		public AOPContainer(IEnumerable<Aspect> aspects, params ExportProvider[] providers) : this(aspects, null, providers)
		{
		}

		public AOPContainer(IEnumerable<Aspect> aspects, ComposablePartCatalog catalog, params ExportProvider[] providers)
			: this(aspects, catalog, false, providers)
		{
		}

		public AOPContainer(IEnumerable<Aspect> aspects, ComposablePartCatalog catalog, bool isThreadSafe, params ExportProvider[] providers)
			: base(catalog, isThreadSafe, providers)
		{
		    Contract.Requires(aspects != null, "aspects");
			Aspects = aspects.ToReadOnlyCollection();
		}

		protected override IEnumerable<Export> GetExportsCore(
			ImportDefinition definition, AtomicComposition atomicComposition)
		{
 
			var exports = base.GetExportsCore(definition, atomicComposition);
 
			foreach (var export in  exports)
			{
				yield return new Export(export.Definition, CreateTransparentProxyGetter(export));
			}
		}

		private Func<object> CreateTransparentProxyGetter(Export export)
		{
			return () =>
			       {
			       	var exportedValue = export.Value;
			       	if (exportedValue == null)
			       	{
			       		return null;
			       	}
			       	var typeName = export.GetExportTypeName();
			       	if (string.IsNullOrEmpty(typeName))
			       	{
			       		return exportedValue;
			       	}
			       	var type = exportedValue.GetType();
			       	var contractType = type.GetInterfaces().FirstOrDefault(interfaceType => interfaceType.FullName == typeName);
			       	if (contractType == null || !contractType.IsInterface)
			       	{
			       		return exportedValue;
			       	}
			       	var interceptors = Aspects.Where(aspect => aspect.Pointcut(export)).Select(aspect => aspect.AopFilter);
			       	if (!interceptors.Any())
			       	{
			       		return exportedValue;
			       	}
			       	var proxy = new RealProxyWrapper(contractType, exportedValue);
			       	return proxy.GetTransparentProxy();
			       };
		}
	}
}