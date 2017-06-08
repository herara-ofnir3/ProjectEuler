using ProjectEuler.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Run.Problems.Part2
{
	/// <summary>
	/// Problem 49 「素数数列」
	/// 項差3330の等差数列1487, 4817, 8147は次の2つの変わった性質を持つ.
	/// 
	/// (i)3つの項はそれぞれ素数である. 
	/// (ii)各項は他の項の置換で表される. 
	/// 1, 2, 3桁の素数にはこのような性質を持った数列は存在しないが, 4桁の増加列にはもう1つ存在する.
	/// 
	/// それではこの数列の3つの項を連結した12桁の数を求めよ.
	/// </summary>
	public class Problem49 : Problem
	{
		public override string Run()
		{
			var primes = Primes(10000 - 1)
				.SkipWhile(p => p < 1000)
				.ToList();

			return string.Empty;
		}

		static IEnumerable<IEnumerable<int>> Comb(IEnumerable<int> items, int r)
		{
			if (r == 0)
				yield return Enumerable.Empty<int>();

			var i = 1;
			foreach (var x in items)
			{
				var xs = items.Skip(i);
				foreach (var c in Comb(xs, r - 1))
					yield return c.AddBefore(x);

				i++;
			}
		}

		static IEnumerable<int> Primes(int max)
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
	}
}
