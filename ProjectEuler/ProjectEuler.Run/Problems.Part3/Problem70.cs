using ProjectEuler.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Run.Problems.Part3
{
	/// <summary>
	/// Problem 70 「トーティエント関数の置換」
	/// オイラーのトーティエント関数 φ(n) (ファイ関数とも呼ばれる) とは, n 未満の正の整数で n と互いに素なものの個数を表す. 例えば, 1, 2, 4, 5, 7, 8 は9未満で9と互いに素であるので, φ(9) = 6 となる. 
	/// 1 は全ての正の整数と互いに素であるとみなされる. よって φ(1) = 1 である.
	/// 
	/// 面白いことに, φ(87109)=79180 であり, 87109は79180を置換したものとなっている.
	/// 
	/// 1 &lt; n &lt; 10^7 で φ(n) が n を置換したものになっているもののうち, n/φ(n) が最小となる n を求めよ.
	/// </summary>
	public class Problem70 : Problem
	{
		const int NMax = 10000000;

		public override string Run()
		{
			// 激遅くん
			var answer = Enumerable.Range(1, NMax).Skip(1)
				.Select(n => new { n, t = Totient(n) })
				.Where(x => IsPerm(x.n, x.t))
				.OrderBy(x => x.n / (double)x.t)
				.First();

			return answer.ToString();
		}

		static bool IsPerm(int x, int y) => 
			x.Digits().OrderBy(xd => xd).SequenceEqual(
				y.Digits().OrderBy(yd => yd));

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
