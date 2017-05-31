using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler.Run.Problems
{
	/// <summary>
	/// 最長のコラッツ数列
	/// 
	/// 正の整数に以下の式で繰り返し生成する数列を定義する.
	/// n → n/2 (n が偶数)
	/// n → 3n + 1 (n が奇数)
	/// 
	/// 13からはじめるとこの数列は以下のようになる.
	/// 13 → 40 → 20 → 10 → 5 → 16 → 8 → 4 → 2 → 1
	/// 
	/// 13から1まで10個の項になる.この数列はどのような数字からはじめても最終的には 1 になると考えられているが, まだそのことは証明されていない(コラッツ問題)
	/// 
	/// さて, 100万未満の数字の中でどの数字からはじめれば最長の数列を生成するか.
	/// 注意: 数列の途中で100万以上になってもよい
	/// </summary>
	public class Problem14 : Problem
	{
		public override string Run()
		{
			var longest = new List<long>();

			for (var i = 1; i < 1000000; i++)
			{
				if (longest.Contains(i))
					continue;

				var numbers = CollatzSeq(i).ToList();

				if (longest.Count < numbers.Count)
				{
					longest = numbers;
				}
			}

			var answer = longest.First();
			return answer.ToString();
		}

		static IEnumerable<long> CollatzSeq(long start)
		{
			var n = start;
			yield return n;

			while (n != 1)
			{
				if (n % 2 == 0)
				{
					n /= 2;
				}
				else
				{
					n = 3 * n + 1;
				}

				yield return n;
			}
		}
	}
}
