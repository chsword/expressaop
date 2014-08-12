
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ExpressAOP {
     static class EnumerableExtensions {
        /// <summary>
        /// 解决foreach时可能数据为null的问题
        /// <example>
        /// foreach(var item in list.ToNotNull()){ /* ... */ }
        /// </example>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ie"></param>
        /// <returns></returns>
        public static IEnumerable<T> ToNotNull<T>(this IEnumerable<T> ie)
        {
            return ie ?? new List<T>();
        }
        /// <summary>
        /// 将集合转换为只读集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static ReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> source)
        {
            Contract.Requires(source != null);
            var readOnlyCollection = source as ReadOnlyCollection<T>;
            if (readOnlyCollection != null)
            {
                return readOnlyCollection;
            }
            return new ReadOnlyCollection<T>(source.ToList());
        }
    }
}
