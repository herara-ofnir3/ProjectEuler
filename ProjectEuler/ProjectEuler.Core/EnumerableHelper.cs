using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Core
{
	public static class EnumerableHelper
	{
		public static IEnumerable<IEnumerable<T>> Perm<T>(this IEnumerable<T> items, int k = -1)
		{
			if (k == -1)
				k = items.Count();

			if (k == 0)
				yield return Enumerable.Empty<T>();

			var i = 0;
			foreach (var x in items)
			{
				var xs = items.Where((_, index) => i != index);
				foreach (var c in Perm(xs, k - 1))
					yield return c.Before(x);

				i++;
			}
		}

		public static IEnumerable<T> Before<T>(this IEnumerable<T> items, T first)
		{
			yield return first;

			foreach (var i in items)
				yield return i;
		}

		public static IEnumerable<T> After<T>(this IEnumerable<T> items, T last)
		{
			foreach (var i in items)
				yield return i;

			yield return last;
		}

		public static IEnumerable<T> FromSingle<T>(T item)
		{
			yield return item;
		}
	}
}
