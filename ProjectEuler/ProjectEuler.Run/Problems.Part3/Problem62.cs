using ProjectEuler.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Run.Problems.Part3
{
	/// <summary>
	/// Problem 62 「立方数置換」
	/// 立方数 41063625 (345^3) は, 桁の順番を入れ替えると2つの立方数になる: 56623104 (384^3) と 66430125 (405^3) である. 41063625は, 立方数になるような桁の置換をちょうど3つもつ最小の立方数である.
	/// 
	/// 立方数になるような桁の置換をちょうど5つもつ最小の立方数を求めよ.
	/// </summary>
	public class Problem62 : Problem
	{
		public override string Run()
		{
			var map = new Dictionary<string, List<long>>();

			foreach (var c in Numbers().Select(n => Pow(n, 3)))
			{
				var digits = string.Join("", c.Digits().OrderBy(x => x));

				if (!map.ContainsKey(digits))
					map[digits] = new List<long>();

				map[digits].Add(c);

				if (map[digits].Count == 5)
					return map[digits].First().ToString();
			}

			throw new Exception();
		}

		static long Pow(int b, int e)
		{
			return Enumerable.Range(1, e).Select(_ => (long)b).Aggregate((l, r) => l * r);
		}

		static IEnumerable<int> Numbers() { for (var n = 1; ; n++) yield return n; }
	}
}
