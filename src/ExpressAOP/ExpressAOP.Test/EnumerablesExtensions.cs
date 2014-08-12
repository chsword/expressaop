using System.Collections.Generic;

namespace ExpressAOP.Test
{
    internal static class  EnumerablesExtensions
    {
         public static string GetStr<T>(this IEnumerable<T> ienum)
         {
             return string.Join(",", ienum);
         }
    }
}