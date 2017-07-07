using ProjectEuler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler.Run.Problems.Part3
{
	/// <summary>
	/// Problem 60 「素数ペア集合」
	/// 素数3, 7, 109, 673は非凡な性質を持っている. 任意の2つの素数を任意の順で繋げると, また素数になっている. 例えば, 7と109を用いると, 7109と1097の両方が素数である. これら4つの素数の和は792である. これは, このような性質をもつ4つの素数の集合の和の中で最小である.
	/// 
	/// 任意の2つの素数を繋げたときに別の素数が生成される, 5つの素数の集合の和の中で最小のものを求めよ.
	/// </summary>
	public class Problem60 : Problem
	{
		public override string Run()
		{
			// 難しすぎ。
			// 4つなら動くが5つだと遅すぎる。
			var map = new Dictionary<int, HashSet<int>>();

			foreach (var prime in Prime.Gen(1000000).SkipWhile(p => p < 10))
			{
				foreach (var pair in PrimePairs(prime))
				{
					if (!map.ContainsKey(pair.left))
						map[pair.left] = new HashSet<int>();

					if (!map.ContainsKey(pair.right))
						map[pair.right] = new HashSet<int>();

					map[pair.left].Add(pair.right);
					map[pair.right].Add(pair.left);
				}
			}

			var list = Search(map, new int[] { }, 4).Select(x => x.ToArray()).ToArray();
			var answer = list.Min(x => x.Sum());
			return answer.ToString();
		}

		static IEnumerable<(int left, int right)> PrimePairs(int prime)
		{
			var digits = prime.Digits().ToArray();
			for (var i = 1; i < digits.Length; i++)
			{
				var leftDigits = digits.Take(i);
				var rightDigits = digits.Skip(i);
				if (leftDigits.Last() == 0)
					continue;

				var left = leftDigits.DigitsToInt();
				var right = rightDigits.DigitsToInt();
				if (left.IsPrime() && right.IsPrime())
				{
					var swap = rightDigits.Concat(leftDigits).DigitsToInt();
					if (swap.IsPrime())
						yield return (left, right);
				}
			}
		}

		static IEnumerable<IEnumerable<int>> Search(Dictionary<int, HashSet<int>> map, int[] values, int len)
		{
			if (len == 0)
			{
				yield return Enumerable.Empty<int>();
			}
			else
			{
				IEnumerable<int> nexts = Enumerable.Empty<int>();
				if (values.Length == 0)
				{
					nexts = map.Keys;
				}
				else
				{
					var prev = values.First();
					nexts = map[prev]
						.Except(values)
						.Where(n => values.Skip(1).All(v => map.ContainsKey(v) && map[n].Contains(v)))
						.ToArray();
				}

				foreach (var n in nexts)
				{
					foreach (var x in Search(map, values.Before(n).ToArray(), len - 1))
						yield return x.Before(n);
				}
			}
		}
	}
}
