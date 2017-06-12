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

			// 桁を置き換えて目的の数の素数を見つけられるパターンを探します。
			foreach (var prime in Prime.Gen())
			{
				var pdigits = prime.Digits();

				// 置き換える桁のインデックスを生成する。
				// 例えば 56003(index=>43210) なら一桁ずつ試すパターン＋同じ値の桁(0)が二つあるので、
				// [ [4(5)], [3(6)], [2(0)], [1(0)], [0(3)], [2(0), 1(0)] ] の桁置換を試します。
				var indexes = pdigits
					.Select((d, i) => new { d, i })
					.GroupBy(x => x.d, x => x.i)
					.SelectMany(x => x.SelectMany((y, i) => x.Comb(i + 1)));

				// [0-9]の桁と上で生成したインデックスをかけ合わせて全ての置換パターンを生成します。
				var patterns = indexes
					.Select(i => (
						index: i, 
						other: digits
							.Select(d => Rep(pdigits, i, d).DigitsToInt())
							.Where(r => r.Digits().Count() == pdigits.Count())
							.Where(r => r.IsPrime())));

				// パターンの中に目的の項数のものがあれば処理を終了します。
				if (patterns.Any(x => x.other.Count() == 8))
				{
					answer = prime;
					break;
				}
			}

			return answer.ToString();
		}

		static IEnumerable<int> Rep(IEnumerable<int> items, IEnumerable<int> indexes, int n)
		{
			var i = 0;
			foreach (var item in items)
			{
				if (indexes.Contains(i))
					yield return n;
				else
					yield return item;
				i++;
			}
		}
	}
}
