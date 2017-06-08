using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler.Run.Problems.Part2
{
	/// <summary>
	/// /// Problem 45 「三角数, 五角数, 六角数」 †
	/// 三角数, 五角数, 六角数は以下のように生成される.
	/// 
	/// 三角数	Tn=n(n+1)/2	1, 3, 6, 10, 15, ...
	/// 五角数	Pn=n(3n-1)/2	1, 5, 12, 22, 35, ...
	/// 六角数	Hn=n(2n-1)	1, 6, 15, 28, 45, ...
	/// T285 = P165 = H143 = 40755であることが分かる.
	/// 
	/// 次の三角数かつ五角数かつ六角数な数を求めよ.
	/// </summary>
	public class Problem45 : Problem
	{
		public override string Run()
		{
			var answer = Gen().Skip(2).First();
			return answer.ToString();
		}

		IEnumerable<long> Gen()
		{
			var pc = new Dictionary<long, int>();
			var pnlast = 0;

			var hc = new Dictionary<long, int>();
			var hnlast = 0;

			foreach (var t in GenT())
			{
				if (pc.ContainsKey(t.val) && hc.ContainsKey(t.val))
				{
					yield return t.val;
					continue;
				}

				var ps = pc.ContainsKey(t.val) ? pc[t.val] : pnlast + 1;
				foreach (var p in GenP(ps).TakeWhile(x => x.val <= t.val))
				{
					var hs = hc.ContainsKey(t.val) ? hc[t.val] : hnlast + 1;
					foreach (var h in GenH(hs).TakeWhile(x => x.val <= t.val))
					{
						if (t.val == p.val && t.val == h.val)
							yield return t.val;

						hc.Add(h.val, h.n);
						hnlast = h.n;
					}

					pc.Add(p.val, p.n);
					pnlast = p.n;
				}
			}
		}

		static long T(long n) => (n * (n + 1)) / 2;
		static long P(long n) => (n * (3 * n - 1)) / 2;
		static long H(long n) => n * (2 * n - 1);

		static IEnumerable<(int n, long val)> GenT(int start = 1)
		{
			for (var n = start; ; n++) yield return (n, T(n));
		}

		static IEnumerable<(int n, long val)> GenP(int start = 1)
		{
			for (var n = start; ; n++) yield return (n, P(n));
		}

		static IEnumerable<(int n, long val)> GenH(int start = 1)
		{
			for (var n = start; ; n++) yield return (n, H(n));
		}
	}
}
