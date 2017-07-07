using ProjectEuler.Core;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace ProjectEuler.Run.Problems.Part3
{
	/// <summary>
	/// Problem 74 「桁の階乗による連鎖」 
	/// 145は各桁の階乗の和が145と自分自身に一致することで有名である.
	/// 
	/// 1! + 4! + 5! = 1 + 24 + 120 = 145
	/// 
	/// 169の性質はあまり知られていない. これは169に戻る数の中で最長の列を成す. このように他の数を経て自分自身に戻るループは3つしか存在しない.
	/// 
	/// 169 → 363601 → 1454 → 169
	/// 871 → 45361 → 871
	/// 872 → 45362 → 872
	/// 
	/// どのような数からスタートしてもループに入ることが示せる.
	/// 
	/// 例を見てみよう.
	/// 
	/// 69 → 363600 → 1454 → 169 → 363601 (→ 1454)
	/// 78 → 45360 → 871 → 45361 (→ 871)
	/// 540 → 145 (→ 145)
	/// 
	/// 69から始めた場合, 列は5つの循環しない項を持つ. また100万未満の数から始めた場合最長の循環しない項は60個であることが知られている.
	/// 
	/// 100万未満の数から開始する列の中で, 60個の循環しない項を持つものはいくつあるか?
	/// </summary>
	public class Problem74 : Problem
	{
		public override string Run()
		{
			// 激遅くん
			var max = 1000000 - 1;

			var answer = Enumerable.Range(1, max)
				.Count(n => NonLoopPart(n).Count() == 60);

			return answer.ToString();
		}

		static int[][] Loops = new int[][]
		{
			new int[] { 169, 363601, 1454 },
			new int[] { 871, 45361 },
			new int[] { 872, 45362 },
		};

		static IEnumerable<BigInteger> NonLoopPart(BigInteger start)
		{
			BigInteger prev = 0;
			BigInteger loopFactor = 0;
			foreach (var n in Chains(start))
			{
				if (n == prev)
					yield break;

				if (loopFactor != 0 && n == loopFactor)
					yield break;

				yield return n;

				if (loopFactor == 0)
					loopFactor = Loops.SelectMany(l => l).FirstOrDefault(l => n == l);

				prev = n;
			}
		}

		static IEnumerable<BigInteger> Chains(BigInteger start)
		{
			yield return start;

			foreach (var c in Chains(F(start)))
				yield return c;
		}

		static BigInteger F(BigInteger n) => n.Digits().Select(d => Factorial(d)).Aggregate((a, b) => a + b);

		static BigInteger Factorial(BigInteger num)
		{
			if (num == 0) return 1;
			return Numbers().TakeWhile(n => n <= num).Aggregate((a, b) => a * b);
		}

		static IEnumerable<BigInteger> Numbers()
		{
			for (var n = 1; ; n++) yield return n;
		}
	}
}
