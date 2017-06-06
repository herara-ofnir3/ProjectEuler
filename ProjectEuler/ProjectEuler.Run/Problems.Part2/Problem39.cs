using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Run.Problems.Part2
{
	/// <summary>
	/// Problem 39 「整数の直角三角形」
	/// 辺の長さが {a,b,c} と整数の3つ組である直角三角形を考え, その周囲の長さを p とする. p = 120のときには3つの解が存在する:
	/// 
	/// {20,48,52}, {24,45,51}, {30,40,50}
	/// 
	/// p ≤ 1000 のとき解の数が最大になる p はいくつか?
	/// </summary>
	public class Problem39 : Problem
	{
		public override string Run()
		{
			var answer = Enumerable.Range(1, 1000)
				.Reverse()
				.Select(n => new { P = n, count = F(n).Count() })
				.OrderByDescending(x => x.count)
				.First()
				.P;

			return answer.ToString();
		}

		static IEnumerable<(int a, int b, int c)> F(int p)
		{
			var aMin = 1;

			for (var a = aMin; a < p ; a++)
			{
				var bMin = a + 1;
				var bMax = (int)Math.Floor((p - a) / 2d);

				for (var b = bMin; b <= bMax ; b++)
				{
					var c = p - a - b;

					if (!(a < b && b < c))
						break;

					if (Math.Pow(a, 2) + Math.Pow(b, 2) == Math.Pow(c, 2))
						yield return (a, b, c);
				}
			}
		}
	}
}
