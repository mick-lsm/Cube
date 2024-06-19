using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cube.Utils
{
    public static class EnumerableExtension
    {
        public static TSource PickRandomElement<TSource>(this IEnumerable<TSource> source)
        {
            var ran = UnityEngine.Random.Range(0, source.Count() - 1);
            return source.ToArray()[ran];
        }
    }
}
