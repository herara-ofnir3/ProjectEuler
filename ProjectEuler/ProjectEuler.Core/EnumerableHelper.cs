﻿using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Core
{
	public static class EnumerableHelper
	{
		public static IEnumerable<IEnumerable<T>> Perm<T>(this IEnumerable<T> items, int? k = null)
		{
			if (k == null)
				k = items.Count();

			if (k == 0)
			{
				yield return Enumerable.Empty<T>();
			}
			else
			{
				var i = 0;
				foreach (var x in items)
				{
					var xs = items.Where((_, index) => i != index);
					foreach (var c in Perm(xs, k - 1))
						yield return c.Before(x);

					i++;
				}
			}
		}

		public static IEnumerable<IEnumerable<T>> Comb<T>(this IEnumerable<T> items, int r)
		{
			if (r == 0)
			{
				yield return Enumerable.Empty<T>();
			}
			else
			{
				var i = 1;
				foreach (var x in items)
				{
					var xs = items.Skip(i);
					foreach (var c in Comb(xs, r - 1))
						yield return c.Before(x);

					i++;
				}
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

		public static IEnumerable<IEnumerable<T>> Chunks<T>(this IEnumerable<T> items, int size)
		{
			if (items.Any())
			{
				yield return items.Take(size);
				foreach (var c in Chunks(items.Skip(size), size))
					yield return c;
			}
		}
	}
}
