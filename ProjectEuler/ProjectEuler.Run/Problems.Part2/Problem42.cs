using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProjectEuler.Run.Problems.Part2
{
	/// <summary>
	/// Problem 42 「符号化三角数」
	/// 三角数のn項は tn = ½n(n+1)で与えられる. 最初の10項は
	/// 
	/// 1, 3, 6, 10, 15, 21, 28, 36, 45, 55, ...
	/// である.
	/// 
	/// 単語中のアルファベットを数値に変換した後に和をとる. この和を「単語の値」と呼ぶことにする. 例えば SKY は 19 + 11 + 25 = 55 = t10である. 単語の値が三角数であるとき, その単語を三角語と呼ぶ.
	/// 
	/// 16Kのテキストファイル words.txt 中に約2000語の英単語が記されている. 三角語はいくつあるか?
	/// </summary>
	public class Problem42 : Problem
	{
		public override string Run()
		{
			var source = Enumerable.Range(65, 26)
				.Select((n, i) => new { c = (char)n, n = i + 1 });

			var map = new Dictionary<char, int>();
			foreach (var s in source)
				map.Add(s.c, s.n);

			var raw = string.Empty;
			using (var fs = new FileStream("Assets\\Problem42\\words.txt", FileMode.Open))
			{
				using (var reader = new StreamReader(fs))
				{
					raw = reader.ReadToEnd();
				}
			}

			var words = raw
				.Split(',')
				.Select(n => n.Trim('"'))
				.OrderBy(n => n)
				.ToList();

			var answer = words
				.Select(w => w.Select(c => map[c]).Sum())
				.Count(x => IsTriangular(x));

			return answer.ToString();
		}

		static int T(int n) => (n * (n + 1)) / 2;

		static IEnumerable<int> Triangulars()
		{
			for (var n = 1; ; n++)
				yield return T(n);
		}

		static bool IsTriangular(int n)
		{
			return Triangulars()
				.TakeWhile(t => t <= n)
				.Contains(n);
		}
	}
}
