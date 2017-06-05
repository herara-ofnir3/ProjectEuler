using ProjectEuler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler.Run.Problems.Part2
{
	/// <summary>
	/// Problem 32 「パンデジタル積」 
	/// すべての桁に 1 から n が一度だけ使われている数をn桁の数がパンデジタル (pandigital) であるということにしよう: 例えば5桁の数 15234 は1から5のパンデジタルである.
	/// 
	/// 7254 は面白い性質を持っている. 39 × 186 = 7254 と書け, 掛けられる数, 掛ける数, 積が1から9のパンデジタルとなる.
	/// 
	/// 掛けられる数/掛ける数/積が1から9のパンデジタルとなるような積の総和を求めよ.
	/// 
	/// HINT: いくつかの積は, 1通り以上の掛けられる数/掛ける数/積の組み合わせを持つが1回だけ数え上げよ. 
	/// </summary>
	public class Problem32 : Problem
	{
		public override string Run()
		{
			var n = 9;

			var patterns = new List<(int a, int b, int c)>();
			for (var ad = 1; ad <= n - 2; ad++)
			{
				for (var bd = 1; bd <= n - ad - 1; bd++)
				{
					var cd = n - ad - bd;

					var aRange = (min: (int)Math.Pow(10, ad - 1), max: (int)Math.Pow(10, ad) - 1);
					var bRange = (min: (int)Math.Pow(10, bd - 1), max: (int)Math.Pow(10, bd) - 1);
					var cRange = (min: (int)Math.Pow(10, cd - 1), max: (int)Math.Pow(10, cd) - 1);

					if (aRange.min * bRange.min <= cRange.min &&
						cRange.max <= aRange.max * bRange.max)
					{
						for (var a = aRange.min; a <= aRange.max; a++)
							for (var b = bRange.min; b <= bRange.max; b++)
								if (IsPandigital(a, b))
									patterns.Add((a, b, a * b));
					}
				}
			}

			var answer = patterns
				.Select(x => x.c)
				.Distinct()
				.Sum();

			return answer.ToString();
		}
		
		static int[] Sample = Enumerable.Range(1, 9).ToArray();

		static bool IsPandigital(int a, int b)
		{
			var c = a * b;
			var digits = a.Digits().Concat(b.Digits()).Concat(c.Digits()).OrderBy(x => x);
			return digits.SequenceEqual(Sample);
		}
	}
}
