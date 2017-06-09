using ProjectEuler.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Run.Problems.Part2
{
	/// <summary>
	/// Problem 49 「素数数列」
	/// 項差3330の等差数列1487, 4817, 8147は次の2つの変わった性質を持つ.
	/// 
	/// (i)3つの項はそれぞれ素数である. 
	/// (ii)各項は他の項の置換で表される. 
	/// 1, 2, 3桁の素数にはこのような性質を持った数列は存在しないが, 4桁の増加列にはもう1つ存在する.
	/// 
	/// それではこの数列の3つの項を連結した12桁の数を求めよ.
	/// </summary>
	public class Problem49 : Problem
	{
		public override string Run()
		{
			// 4桁の素数を全て求める。
			var primes = Primes(10000 - 1)
				.SkipWhile(p => p < 1000)
				.OrderBy(p => p)
				.ToList();

			// 同じ数字の桁で構成される素数をグループ化する。
			// 答えは3項の等差数列なので、3項以下のグループは捨てる。
			var sameDigits = primes
				.Select(p => (n: p, d: string.Join("", p.Digits().OrderBy(d => d))))
				.GroupBy(x => x.d, x => x.n)
				.Where(x =>  3 <= x.Count())
				.ToList();

			var list = new List<(int a, int b, int c)>();
			foreach (var seq in sameDigits)
			{
				// グループ内から等差数列を探す。
				// グループ化された数字から3項の組み合わせを列挙し、等差数列になっているか判定する。
				var l = Comb(seq, 3)
					.Select(comb => comb.OrderBy(c => c))
					.Select(comb => (a: comb.ElementAt(0), b: comb.ElementAt(1), c: comb.ElementAt(2)))
					.Where(x => x.c - x.b == x.b - x.a)
					;

				list.AddRange(l);
			}

			// 見つかった2つのうち、問題文に明記されている方を除外したら答えが得られる
			var answer = list.Single(x => !x.Equals((1487, 4817, 8147)));
			return answer.ToString();
		}

		static IEnumerable<IEnumerable<T>> Comb<T>(IEnumerable<T> items, int r)
		{
			if (r == 0)
				yield return Enumerable.Empty<T>();

			var i = 1;
			foreach (var x in items)
			{
				var xs = items.Skip(i);
				foreach (var c in Comb(xs, r - 1))
					yield return c.Before(x);

				i++;
			}
		}

		static IEnumerable<int> Primes(int max)
		{
			var map = new Dictionary<int, bool>();
			foreach (var l in Enumerable.Range(2, max - 1))
				map.Add(l, true);

			var head = 0;
			var sqrt = Math.Sqrt(max);

			while (head <= sqrt)
			{
				head = map
					.Where(x => x.Value)
					.First(x => head < x.Key)
					.Key;

				yield return head;

				var removes = Multiples(head)
					.Skip(1)
					.TakeWhile(x => x <= max);

				foreach (var r in removes)
					map[r] = false;
			}

			foreach (var m in map.Where(m => head < m.Key && m.Value))
				yield return m.Key;
		}

		static IEnumerable<int> Multiples(int n)
		{
			for (var m = n; ; m += n)
			{
				yield return m;
			}
		}
	}
}
