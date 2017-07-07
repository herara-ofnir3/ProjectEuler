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
	/// Problem 56 「もっとべき乗の数字和」
	/// Googol (10^100)は非常に大きな数である: 1の後に0が100個続く. 100^100は想像を絶する. 1の後に0が200回続く. その大きさにも関わらず, 両者とも数字和 ( 桁の和 ) は1である.
	/// 
	/// a, b &lt; 100 について自然数 a^b を考える. 数字和の最大値を答えよ.
	/// </summary>
	public class Problem56 : Problem
	{
		public override string Run()
		{
			// BigInteger強すぎ問題
			var range = Enumerable.Range(1, 100 - 1);
			var patterns = new List<BigInteger>();

			foreach (var a in range)
				foreach (var b in range)
					patterns.Add(Pow(a, b));

			var answer = patterns.Max(p => p.Digits().Sum());
			return answer.ToString();
		}

		static BigInteger Pow(int b, int e)
		{
			return Enumerable.Range(1, e)
				.Select(_ => (BigInteger)b)
				.Aggregate((l, r) => l * r);
		}
	}
}
