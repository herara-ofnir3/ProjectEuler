using ProjectEuler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler.Run.Problems.Part3
{
	/// <summary>
	/// Problem 69 「トーティエント関数の最大値」
	/// オイラーのトーティエント関数, φ(n) [時々ファイ関数とも呼ばれる]は, n と互いに素な n 未満の数の数を定める. たとえば, 1, 2, 4, 5, 7, そして8はみな9未満で9と互いに素であり, φ(9)=6.
	/// 
	/// n	互いに素な数	φ(n)	n/φ(n)
	/// 2	1	1	2
	/// 3	1,2	2	1.5
	/// 4	1,3	2	2
	/// 5	1,2,3,4	4	1.25
	/// 6	1,5	2	3
	/// 7	1,2,3,4,5,6	6	1.1666...
	/// 8	1,3,5,7	4	2
	/// 9	1,2,4,5,7,8	6	1.5
	/// 10	1,3,7,9	4	2.5
	/// n ≤ 10 では n/φ(n) の最大値は n=6 であることがわかる.
	/// 
	/// n ≤ 1,000,000で n/φ(n) が最大となる値を見つけよ.
	/// </summary>
	public class Problem69 : Problem
	{
		const int NMax = 1000000;

		public override string Run()
		{
			// ちょい遅
			var answer = Enumerable.Range(1, NMax).Skip(1)
				.Select(n => new { n, t = Totient(n) })
				.OrderByDescending(x => x.n / (double)x.t)
				.First();

			return answer.ToString();
		}

		static HashSet<int> Primes = new HashSet<int>(Prime.Gen(NMax));

		static int Totient(int n)
		{
			// nが素数の場合は、n以下の全ての数と互いに素なので T = n - 1 となる。
			if (Primes.Contains(n))
				return n - 1;

			// 素数pのk乗の数の場合、その数の T は p^k - p^(k-1) となる。
			// これを利用して n を素因数分解し、各項の T を掛け合わせることで解を得る。
			return PrimeFactors(n)
				.Select(x => F(x.b, x.e))
				.Aggregate((a, b) => a * b);
		}

		static int F(int p, int k)
		{
			return (int)Math.Pow(p, k) - (int)Math.Pow(p, k - 1);
		}

		static IEnumerable<(int b, int e)> PrimeFactors(int n)
		{
			return PrimeFactorsRec(n)
				.GroupBy(x => x)
				.Select(g => (g.Key, g.Count()));
		}

		static IEnumerable<int> PrimeFactorsRec(int n)
		{
			if (n <= 1)
				yield break;

			int prime;

			if (Primes.Contains(n))
				prime = n;
			else
				prime = Primes.TakeWhile(p => p <= n).First(p => n % p == 0);

			yield return prime;

			foreach (var p in PrimeFactorsRec(n / prime))
				yield return p;
		}
	}
}
