using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectEuler.Core;

namespace ProjectEuler.Run.Problems.Part2
{
	/// <summary>
	/// Problem 47 「異なる素因数」
	/// それぞれ2つの異なる素因数を持つ連続する2つの数が最初に現れるのは:
	/// 
	/// 14 = 2 × 7
	/// 15 = 3 × 5
	/// 
	/// それぞれ3つの異なる素因数を持つ連続する3つの数が最初に現れるのは:
	/// 
	/// 644 = 2^2 × 7 × 23
	/// 645 = 3 × 5 × 43
	/// 646 = 2 × 17 × 19
	/// 
	/// 最初に現れるそれぞれ4つの異なる素因数を持つ連続する4つの数を求めよ. その最初の数はいくつか?
	/// </summary>
	public class Problem47 : Problem
	{
		public override string Run()
		{
			var seqCount = 4;
			var answer = 0;

			for (var n = 1; ; n++)
			{
				var seq = Enumerable.Range(0, seqCount)
					.Select(i => (n: n + i, pf: PrimeFactors(n + i)));

				if (seq.Any(s => s.pf.Count() != seqCount))
					continue;

				var all = seq.SelectMany(x => x.pf).Count();
				var distinct = seq.SelectMany(x => x.pf).Distinct().Count();

				if (all == distinct)
				{
					answer = seq.First().n;
					break;
				}
			}

			return answer.ToString();
		}

		Dictionary<int, IEnumerable<(int b, int e)>> _primeFactors = new Dictionary<int, IEnumerable<(int b, int e)>>();

		IEnumerable<(int b, int e)> PrimeFactors(int n)
		{
			if (!_primeFactors.ContainsKey(n))
			{
				var pf = GenPrimeFactors(n)
					.GroupBy(x => x)
					.Select(g => (g.Key, g.Count()));

				_primeFactors.Add(n, pf);
			}

			return _primeFactors[n];
		}

		IEnumerable<int> GenPrimeFactors(int n)
		{
			var q = n;

			while (1 < q)
			{
				var prime = Primes()
					.TakeWhile(p => p <= q)
					.First(p => q % p == 0);

				yield return prime;
				q /= prime;
			}
		}

		List<int> _primes = new List<int> { 2, 3, 5, 7 };

		IEnumerable<int> Primes()
		{
			var last = 0;
			foreach (var p in _primes)
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
	}
}
