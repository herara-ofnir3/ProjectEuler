using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Core
{
	public static class EnumerableHelper
	{
		public static IEnumerable<IEnumerable<T>> Perm<T>(this IEnumerable<T> items, int k = -1)
		{
			if (k == -1)
			{
				k = items.Count();
			}

			if (0 < k)
			{
				var i = 0;
				foreach (var item in items)
				{
					var single = true;
					var without = items.Where((x, index) => index != i);

					foreach (var p in without.Perm(k - 1))
					{
						yield return p.AddBefore(item);
						single = false;
					}

					if (single)
						yield return FromSingle(item);

					i++;
				}
			}
		}

		public static IEnumerable<T> AddBefore<T>(this IEnumerable<T> items, T first)
		{
			yield return first;

			foreach (var i in items)
				yield return i;
		}

		public static IEnumerable<T> AddAfter<T>(this IEnumerable<T> items, T last)
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
