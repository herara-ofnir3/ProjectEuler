using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace ProjectEuler.Core
{
	public static class Prime
	{
		public static IEnumerable<int> Gen(int max)
		{
			var map = new Dictionary<int, bool>();
			foreach (var l in Enumerable.Range(2, max - 1))
				map.Add(l, true);

			var head = 0;
			var sqrt = Math.Sqrt(max);

			while (head <= sqrt)
			{
				head = map
					.Where(x => x.Value)
					.First(x => head < x.Key)
					.Key;

				yield return head;

				var removes = Multiples(head)
					.Skip(1)
					.TakeWhile(x => x <= max);

				foreach (var r in removes)
					map[r] = false;
			}

			foreach (var m in map.Where(m => head < m.Key && m.Value))
				yield return m.Key;
		}

		static IEnumerable<int> Multiples(int n)
		{
			for (var m = n; ; m += n)
			{
				yield return m;
			}
		}

		static readonly List<int> _primes = new List<int> { 2, 3, 5, 7 };

		public static IEnumerable<int> Gen()
		{
			var last = 0;
			var cache = _primes.ToList();
			foreach (var p in cache)
			{
				yield return p;
				last = p;
			}

			for (var n = last + 2; ; n += 2)
			{
				if (_primes.TakeWhile(p => p <= Math.Sqrt(n)).Any(p => n % p == 0))
					continue;

				yield return n;
				_primes.Add(n);
			}
		}

		public static bool IsPrime(int n)
		{
			if (n < 2) return false;

			return Gen()
				.TakeWhile(p => p <= Math.Sqrt(n))
				.All(p => n % p != 0);
		}

		public static bool IsPrime(BigInteger n)
		{
			if (n < 2) return false;

			return Gen()
				.TakeWhile(p => p <= Math.Sqrt((double)n))
				.All(p => n % p != 0);
		}
	}
}
