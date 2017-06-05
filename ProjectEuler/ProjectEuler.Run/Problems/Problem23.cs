using ProjectEuler.Core;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Run.Problems
{
	/// <summary>
	/// 非過剰数和
	/// 
	/// 完全数とは, その数の真の約数の和がそれ自身と一致する数のことである. たとえば, 28の真の約数の和は, 1 + 2 + 4 + 7 + 14 = 28 であるので, 28は完全数である.
	/// 
	/// 真の約数の和がその数よりも少ないものを不足数といい, 真の約数の和がその数よりも大きいものを過剰数と呼ぶ.
	/// 
	/// 12は, 1 + 2 + 3 + 4 + 6 = 16 となるので, 最小の過剰数である. よって2つの過剰数の和で書ける最少の数は24である. 数学的な解析により, 28123より大きい任意の整数は2つの過剰数の和で書けることが知られている. 2つの過剰数の和で表せない最大の数がこの上限よりも小さいことは分かっているのだが, この上限を減らすことが出来ていない.
	/// 
	/// 2つの過剰数の和で書き表せない正の整数の総和を求めよ.
	/// </summary>
	public class Problem23 : Problem
	{
		public override string Run()
		{
			var max = 28123;
			var abundants = Enumerable.Range(1, max)
				.Where(n => n < n.TrueDivisors().Sum())
				.ToList();

			var twoAbundantSums = Combine(abundants, abundants)
				.Select(x => x.Sum())
				.Distinct()
				.ToList();

			var answer = Enumerable.Range(1, max)
				.Where(n => !twoAbundantSums.Contains(n))
				.Sum();

			return answer.ToString();
		}

		static IEnumerable<IEnumerable<int>> Combine(IEnumerable<int> items1, IEnumerable<int> items2)
		{
			foreach (var i1 in items1)
			{
				foreach (var i2 in items2)
				{
					yield return new [] { i1, i2 };
				}
			}
		}
	}
}
