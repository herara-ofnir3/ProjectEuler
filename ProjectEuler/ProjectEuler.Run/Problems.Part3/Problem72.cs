using ProjectEuler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler.Run.Problems.Part3
{
	/// <summary>
	/// Problem 72 「分数の数え上げ」 
	/// nとdを正の整数として, 分数 n/d を考えよう. n&lt;d かつ HCF(n,d)=1 のとき, 真既約分数と呼ぶ.
	/// 
	/// d ≤ 8について真既約分数を大きさ順に並べると, 以下を得る:
	/// 
	/// 1/8, 1/7, 1/6, 1/5, 1/4, 2/7, 1/3, 3/8, 2/5, 3/7, 1/2, 4/7, 3/5, 5/8, 2/3, 5/7, 3/4, 4/5, 5/6, 6/7, 7/8
	/// この集合は21個の要素をもつことが分かる.
	/// 
	/// d ≤ 1,000,000について, 真既約分数の集合は何個の要素を持つか?
	/// </summary>
	public class Problem72 : Problem
	{
		const int NMax = 1000000;

		public override string Run()
		{
			// Problem69あたりから同じような問題が続く
			var answer = Enumerable.Range(1, NMax).Skip(1)
				.Select(d => (BigInteger)Totient(d))
				.Aggregate((a, b) => a + b);

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
