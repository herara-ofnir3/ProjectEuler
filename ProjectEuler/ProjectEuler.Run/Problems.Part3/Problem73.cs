using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Run.Problems.Part3
{
	/// <summary>
	/// Problem 73 「ある範囲内の分数の数え上げ」 
	/// nとdを正の整数として, 分数 n/d を考えよう. n&lt;d かつ HCF(n,d)=1 のとき, 真既約分数と呼ぶ.
	/// 
	/// d ≤ 8 について既約分数を大きさ順に並べると, 以下を得る:
	/// 
	/// 1/8, 1/7, 1/6, 1/5, 1/4, 2/7, 1/3, 3/8, 2/5, 3/7, 1/2, 4/7, 3/5, 5/8, 2/3, 5/7, 3/4, 4/5, 5/6, 6/7, 7/8
	/// 1/3と1/2の間には3つの分数が存在することが分かる.
	/// 
	/// では, d ≤ 12,000 について真既約分数をソートした集合では, 1/3 と 1/2 の間に何個の分数があるか?
	/// </summary>
	public class Problem73 : Problem
	{
		public override string Run()
		{
			var dmax = 12000;
			var range = (from: 1 / 3d, to: 1 / 2d);

			var answer = Enumerable.Range(1, dmax).Skip(1)
				.SelectMany(d =>
					Incr((int)(d * range.from))
						.Where(n => Gcd(n, d) == 1)
						.Select(n => n / (double)d)
						.SkipWhile(f => f <= range.from)
						.TakeWhile(f => f < range.to))
				.Count();

			return answer.ToString();
		}

		static IEnumerable<int> Incr(int start)
		{
			for (var n = start; ; n++) yield return n;
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