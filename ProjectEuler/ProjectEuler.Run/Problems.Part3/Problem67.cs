using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProjectEuler.Run.Problems.Part3
{
	/// <summary>
	/// Problem 67 「最大経路の和 その2」
	/// 以下の三角形の頂点から下まで移動するとき, その数値の合計の最大値は23になる.
	/// 
	/// 3
	/// 7 4
	/// 2 4 6
	/// 8 5 9 3
	/// この例では 3 + 7 + 4 + 9 = 23
	/// 
	/// 100列の三角形を含んでいる15Kのテキストファイル triangle.txt (右クリックして, 『名前をつけてリンク先を保存』)の上から下まで最大合計を見つけよ.
	/// 
	/// 注：これは, Problem 18のずっと難しいバージョンです. 
	/// 全部で2^99 通りの組み合わせがあるので, この問題を解決するためにすべてのルートをためすことは可能でありません！
	/// あなたが毎秒1兆本の(10^12)ルートをチェックすることができたとしても, 全てをチェックするために200億年以上かかるでしょう. 
	/// 解決するための効率的なアルゴリズムがあります. ;o)
	/// </summary>
	public class Problem67 : Problem
	{
		public override string Run()
		{
			// メモ化しただけで早くなりすぎてびびる
			var triangle = new List<List<int>>(100);

			using (var fs = new FileStream("Assets\\Problem67\\triangle.txt", FileMode.Open))
			{
				using (var reader = new StreamReader(fs))
				{
					while (-1 < reader.Peek())
						triangle.Add(reader.ReadLine().Split(' ').Select(s => int.Parse(s)).ToList());
				}
			}

			var answer = F(triangle, 0, 0);
			return answer.ToString();
		}

		Dictionary<(int x, int y), int> memo = new Dictionary<(int x, int y), int>();

		int F(List<List<int>> triangle, int x, int y)
		{
			if (memo.ContainsKey((x, y)))
				return memo[(x, y)];

			if (triangle.Count <= y || triangle[y].Count <= x)
				return memo[(x, y)] = 0;

			var value = triangle[y][x];
			var left = F(triangle, x, y + 1);
			var right = F(triangle, x + 1, y + 1);

			return memo[(x, y)] = value + Math.Max(left, right);
		}
	}
}