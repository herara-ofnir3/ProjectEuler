using ProjectEuler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler.Run.Problems.Part3
{
	/// <summary>
	/// Problem 71 「順序分数」
	/// nとdを正の整数として, 分数 n/d を考えよう. n&lt;d かつ HCF(n,d)=1 のとき, 真既約分数と呼ぶ.
	/// 
	/// d ≤ 8について既約分数を大きさ順に並べると, 以下を得る:
	/// 
	/// 1/8, 1/7, 1/6, 1/5, 1/4, 2/7, 1/3, 3/8, 2/5, 3/7, 1/2, 4/7, 3/5, 5/8, 2/3, 5/7, 3/4, 4/5, 5/6, 6/7, 7/8
	/// 3/7のすぐ左の分数は2/5である.
	/// 
	/// d ≤ 1,000,000について真既約分数を大きさ順に並べたとき, 3/7のすぐ左の分数の分子を求めよ.
	/// </summary>
	public class Problem71 : Problem
	{
		const int NMax = 1000000;

		public override string Run()
		{
			var t = 3 / (double)7;
			var answer = Enumerable.Range(1, NMax).Skip(1)
				.Select(d =>
					Decr((int)(d * t), 1) 
						.SkipWhile(n => t <= n / (double)d) 
						.Where(n => Gcd(n, d) == 1)
						.Select(n => new { n, d })
						.FirstOrDefault())
				.Where(x => x != null)
				.OrderBy(x => x.n / (double)x.d)
				.Last();

			return answer.ToString();
		}

		static IEnumerable<int> Decr(int start, int end)
		{
			for (var n = start; end <= n; n--)
				yield return n;
		}

		static int Gcd(int a, int b)
		{
			if (a == 0 || b == 0)
				return 0;

			var h = a;
			var l = b;

			if (h < l)
			{
				h = b;
				l = a;
			}

			var r = h % l;
			while (r != 0)
			{
				h = l;
				l = r;
				r = h % l;
			}

			return l;
		}
	}
}
