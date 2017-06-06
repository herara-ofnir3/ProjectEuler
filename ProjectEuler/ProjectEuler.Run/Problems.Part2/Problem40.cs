using ProjectEuler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler.Run.Problems.Part2
{
	/// <summary>
	/// Problem 40 「チャンパーノウン定数」
	/// 正の整数を順に連結して得られる以下の10進の無理数を考える:
	/// 
	/// 0.123456789101112131415161718192021...
	/// 小数第12位は1である.
	/// 
	/// dnで小数第n位の数を表す. d1 × d10 × d100 × d1000 × d10000 × d100000 × d1000000 を求めよ.
	/// </summary>
	public class Problem40 : Problem
	{
		public override string Run()
		{
			var targets = Enumerable.Range(0, 7)
				.Select(i => Math.Pow(10, i))
				.ToArray();

			var answer = ChampernowneDigits()
				.TakeWhile((d, i) => i <= targets.Max())
				.Where((d, i) => targets.Contains(i + 1))
				.Aggregate((a, b) => a * b);

			return answer.ToString();
		}

		static IEnumerable<int> ChampernowneDigits()
		{
			for (var n = 1; ; n++)
				foreach (var d in n.Digits().Reverse())
					yield return d;
		}
	}
}
