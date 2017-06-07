using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler.Run.Problems.Part2
{
	/// <summary>
	/// Problem 44 「五角数」
	/// 五角数は Pn = n(3n-1)/2 で生成される. 最初の10項は
	/// 
	/// 1, 5, 12, 22, 35, 51, 70, 92, 117, 145, ...
	/// である.
	/// 
	/// P4 + P7 = 22 + 70 = 92 = P8 である. しかし差 70 - 22 = 48 は五角数ではない.
	/// 
	/// 五角数のペア Pj と Pk について, 差と和が五角数になるものを考える. 差を D = |Pk - Pj| と書く. 差 D の最小値を求めよ.
	/// </summary>
	public class Problem44 : Problem
	{
		public override string Run()
		{
			(long j, long k) pair = (0, 0);

			// そのうち見つかるだろうの精神。
			for (var i = 1; pair.j == 0 && pair.k == 0; i++)
			{
				// 最小の差なので、j と k はそんなに離れていない感じがする。
				// だから同じ桁内の計算のみ行う。きっとみつかる。
				var s = (long)Math.Pow(10, i - 1);
				var e = (long)Math.Pow(10, i) - 1;
				pair = GenPair(s, e)
					.FirstOrDefault(p => F(p.j, p.k));
			}

			var answer = D(pair.j, pair.k);
			return answer.ToString();
		}

		static long P(long n) => (n * (3 * n - 1)) / 2;

		static long D(long j, long k) => P(j) - P(k);

		static long S(long j, long k) => P(j) + P(k);

		static bool F(long j, long k) => IsPenta(D(j, k)) && IsPenta(S(j, k));

		static IEnumerable<long> GenP(long start = 1)
		{
			for (var n = start; ; n++)
				yield return P(n);
		}

		static HashSet<long> _pentas = new HashSet<long>();
		static long _last = 0;

		static bool IsPenta(long n)
		{
			if (n <= _last)
				return _pentas.Contains(n);

			foreach (var p in GenP(_pentas.Count + 1).TakeWhile(p => p <= n))
			{
				_pentas.Add(p);
				_last = p;
			}

			return _pentas.Contains(n);
		}

		static IEnumerable<(long j, long k)> GenPair(long start, long end)
		{
			for (var k = start; k < end; k++)
				for (var j = start + 1; j <= end; j++)
					yield return (j, k);
		}
	}
}
