using ProjectEuler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler.Run.Problems.Part3
{
	/// <summary>
	/// Problem 68 「Magic 5-gon ring」
	/// 下に示す図のようなものを"magic" 3-gon ringという. これは1～6の数字を当てはめて, 各列の数字の和が9となっている. これを例として説明する.
	/// 
	/// 外側のノードのうち一番小さいものの付いた列(例では4,3,2)から時計回りに回ってそれぞれ列の数字を3つ連ねて説明する. 例えば例のものは4,3,2; 6,2,1; 5,1,3という組で説明することができる.
	/// 
	/// 1～6の数字を当てはめて, 各列の数字の和が等しくなるものは次の8通りある.
	/// 
	/// 合計	組
	/// 9	4,2,3; 5,3,1; 6,1,2
	/// 9	4,3,2; 6,2,1; 5,1,3
	/// 10	2,3,5; 4,5,1; 6,1,3
	/// 10	2,5,3; 6,3,1; 4,1,5
	/// 11	1,4,6; 3,6,2; 5,2,4
	/// 11	1,6,4; 5,4,2; 3,2,6
	/// 12	1,5,6; 2,6,4; 3,4,5
	/// 12	1,6,5; 3,5,4; 2,4,6
	/// この組の各数字を連結して, 9桁の数字で表すことができる. 例えば, 上の図のものは4,3,2; 6,2,1; 5,1,3であるので432621513である.
	/// 
	/// さて, 下の図に1～10の数字を当てはめ, 各列の数字の和が等しくなる"magic" 5-gon ringを作って, それを表す16桁または17桁の数字のうち, 16桁のものの最大の数字を答えよ.
	/// 
	/// (注, 3つの場合の例を見ても分かる通り, 列の始まりの数字を比べた時一番小さい数字で始まる列から時計回りに繋げるという条件のもとで文字列を生成する必要があります. この条件下で最大となる数字を答えてください. )
	/// </summary>
	public class Problem68 : Problem
	{
		public override string Run()
		{
			// 激遅くん
			var n = 5;
			var nums = Enumerable.Range(1, n * 2).ToArray();

			// リングの内側のパターンを生成する。
			var inner = nums.Perm(n);

			// リングの外側のパターンを作り、内側のパターンと組み合わせる。
			var innerOuter = inner
				.SelectMany(x => 
					nums.Except(x)
						.Perm(n)
						.Select(o => new { inner = x.ToArray(), outer = o.ToArray() }));

			// 列ごとに分割してリングを生成する。
			var rings = innerOuter
				.Select(p => 
					p.inner.Select((i, index) => 
						new int[] { i, p.outer[index], p.outer[(index + 1) % n] }));

			// リングとして成立している(全ての列の合計が同じ)もののみを対象とし、
			// 列を桁に変換して最大値を取得する。
			var answer = rings
				.Where(r => r.Select(c => c.Sum()).All(cs => cs == r.First().Sum()))
				.Select(r => RingSort(r))
				.Select(r => r.SelectMany(c => c.SelectMany(x => x.Digits().Reverse())).Reverse().DigitsToBigInteger())
				.Where(d => d.Digits().Count() == 16)
				.Max();

			return answer.ToString();
		}

		static IEnumerable<IEnumerable<int>> RingSort(IEnumerable<IEnumerable<int>> ring)
		{
			// リングの開始位置を揃える。
			var minFirst = ring.Min(c => c.First());
			var minFirstIndex = ring.Select((c, i) => new { c, i }).Single(x => x.c.First() == minFirst).i;
			return ring.Skip(minFirstIndex)
				.Concat(ring.Take(minFirstIndex));
		}
	}
}
