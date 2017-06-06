using ProjectEuler.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Run.Problems.Part2
{
	/// <summary>
	/// Problem 35 「巡回素数」
	/// 197は巡回素数と呼ばれる. 桁を回転させたときに得られる数 197, 971, 719 が全て素数だからである.
	/// 
	/// 100未満には巡回素数が13個ある: 2, 3, 5, 7, 11, 13, 17, 31, 37, 71, 73, 79, および97である.
	/// 
	/// 100万未満の巡回素数はいくつあるか?
	/// </summary>
	public class Problem35 : Problem
	{
		public override string Run()
		{
			var primes = new HashSet<int>(Primes(1000000 - 1));
			var circularPrimes = primes
				.Where(p => 
					Circulars(p.Digits())
						.Select(c => DigitsToInt(c))
						.All(c => primes.Contains(c)))
				.ToList();
			var answer = circularPrimes.Count();
			return answer.ToString();
		}

		static IEnumerable<IEnumerable<int>> Circulars(IEnumerable<int> digits)
		{
			var count = digits.Count();
			foreach (var i in Enumerable.Range(0, count))
			{
				var left = digits.Skip(i).Take(count - i);
				var right = digits.Take(i);
				yield return left.Concat(right);
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

		static int DigitsToInt(IEnumerable<int> digits)
		{
			return digits.Select((x, i) => x * (int)Math.Pow(10, i)).Sum();
		}
	}
}
