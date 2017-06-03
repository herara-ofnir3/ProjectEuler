using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler.Run.Problems.Part2
{
	/// <summary>
	/// 「二次式素数」 †
	/// オイラーは以下の二次式を考案している:
	/// 
	/// n^2 + n + 41.
	/// この式は, n を0から39までの連続する整数としたときに40個の素数を生成する. しかし, n = 40 のとき 402 + 40 + 41 = 40(40 + 1) + 41 となり41で割り切れる. また, n = 41 のときは 412 + 41 + 41 であり明らかに41で割り切れる.
	/// 
	/// 計算機を用いて, 二次式 n^2 - 79n + 1601 という式が発見できた. これは n = 0 から 79 の連続する整数で80個の素数を生成する. 係数の積は, -79 × 1601 で -126479である.
	/// 
	/// さて, |a| &lt; 1000, |b| ≤ 1000 として以下の二次式を考える (ここで |a| は絶対値): 例えば |11| = 11 |-4| = 4である.
	/// 
	/// n^2 + an + b
	/// n = 0 から始めて連続する整数で素数を生成したときに最長の長さとなる上の二次式の, 係数 a, b の積を答えよ.	
	/// </summary>
	public class Problem27 : Problem
	{
		public override string Run()
		{
			var result = new Dictionary<Tuple<int, int>, int>();

			var aMax = 1000 - 1;
			var aMin = -aMax; 
			var bMax = 1000;
			var bMin = -bMax;

			for (var a = aMin; a <= aMax; a++)
			{
				for (var b = bMin; b <= bMax; b++)
				{
					var primesCount = Gen(a, b).Count();

					if (0 < primesCount)
						result.Add(Tuple.Create(a, b), primesCount);
				}
			}

			var t = result
				.OrderByDescending(x => x.Value)
				.First()
				.Key;

			var answer = t.Item1 * t.Item2;
			return answer.ToString();
		}

		IEnumerable<int> Gen(int a, int b)
		{
			for (var i = 0; ; i++)
			{
				var f = F(i, a, b);

				if (!Primes(f).Contains(f))
					break;

				yield return f;
			}
		}

		static int F(int n, int a, int b)
		{
			return n * n + a * n + b;
		}

		IEnumerable<int> Primes(int n)
		{
			return Primes().TakeWhile(x => x <= n);
		}

		List<int> _primeCaches = new List<int>();

		IEnumerable<int> Primes()
		{
			var firstPrimer = 2;
			var i = firstPrimer;

			foreach (var p in _primeCaches)
			{
				yield return p;
				i = p;
			}

			while (true)
			{
				var isPrime = (!_primeCaches.Any(prime => i % prime == 0));

				if (isPrime)
				{
					yield return i;
					_primeCaches.Add(i);
				}

				i++;
			}
		}
	}
}
