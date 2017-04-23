using System.ComponentModel.Composition.Primitives;

namespace ExpressAOP
{
    internal static class MEFExtensions
	{
		public static string GetExportTypeName(this Export export)
		{
			object value;
			export.Metadata.TryGetValue("ExportTypeIdentity", out value);
			var typeIdentity = value as string;
			if (string.IsNullOrEmpty(typeIdentity))
			{
				return null;
			}
			return string.Join(string.Empty, typeIdentity.Split(' '));
		}
	}
}
