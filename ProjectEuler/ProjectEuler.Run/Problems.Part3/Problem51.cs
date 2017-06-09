using ProjectEuler.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Run.Problems.Part3
{
	/// <summary>
	/// Problem 51 「素数の桁置換」
	/// *3の第1桁を置き換えることで, 13, 23, 43, 53, 73, 83という6つの素数が得られる.
	/// 
	/// 56**3の第3桁と第4桁を同じ数で置き換えることを考えよう. この5桁の数は7つの素数をもつ最初の例である: 56003, 56113, 56333, 56443, 56663, 56773, 56993. よって, この族の最初の数である56003は, このような性質を持つ最小の素数である.
	/// 
	/// 桁を同じ数で置き換えることで8つの素数が得られる最小の素数を求めよ. (注:連続した桁でなくても良い)
	/// </summary>
	public class Problem51 : Problem
	{
		public override string Run()
		{
			var digits = Enumerable.Range(0, 10).ToArray();
			var answer = 0;

			foreach (var prime in Prime.Gen())
			{
				var pdigits = prime.Digits();
				var list = pdigits
					.Select((_, i) => (
						index: i, 
						other: digits.Select(d => Rep(pdigits, i, d))
							.Select(r => r.DigitsToInt())
							.Where(r => r.Digits().Count() == pdigits.Count())
							.Where(r => r.IsPrime())));

				if (list.Any(x => x.other.Count() == 7))
				{
					answer = prime;
					break;
				}
			}

			return answer.ToString();
		}

		static IEnumerable<int> Rep(IEnumerable<int> items, int index, int n)
		{
			var i = 0;
			foreach (var item in items)
			{
				if (i == index)
					yield return n;
				else
					yield return item;
				i++;
			}
		}
	}
}
