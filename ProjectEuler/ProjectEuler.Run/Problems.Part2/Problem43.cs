using ProjectEuler.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Run.Problems.Part2
{
	/// <summary>
	/// Problem 43 「部分文字列被整除性」
	/// 数1406357289は0から9のパンデジタル数である (0から9が1度ずつ現れるので). この数は部分文字列が面白い性質を持っている.
	/// 
	/// d1を上位1桁目, d2を上位2桁目の数とし, 以下順にdnを定義する. この記法を用いると次のことが分かる.
	/// 
	/// d2 d3 d4=406 は 2 で割り切れる
	/// d3 d4 d5=063 は 3 で割り切れる
	/// d4 d5 d6=635 は 5 で割り切れる
	/// d5 d6 d7=357 は 7 で割り切れる
	/// d6 d7 d8=572 は 11 で割り切れる
	/// d7 d8 d9=728 は 13 で割り切れる
	/// d8 d9 d10=289 は 17 で割り切れる
	/// このような性質をもつ0から9のパンデジタル数の総和を求めよ.
	/// </summary>
	public class Problem43 : Problem
	{
		public override string Run()
		{
			// 激遅くん
			var d = Enumerable.Range(0, 10).ToArray();
			var primes = new int[] { 1, 2, 3, 5, 7, 11, 13, 17 };
			var list = new List<IEnumerable<int>>();

			foreach (var pattern in F(d))
			{
				var matched = true;
				foreach (var sub in pattern.Select((n, i) => new { n, i }))
				{
					if (sub.i == 0 && sub.n < 100)
					{
						matched = false;
						break;
					}

					if (sub.i == 0)
						continue;

					if (sub.n % primes[sub.i] != 0)
					{
						matched = false;
						break;
					}
				}

				if (matched)
				{
					list.Add(pattern.ToList());
				}
			}

			var answer = list
				.Select(x => FromSubs(x))
				.Sum();

			return answer.ToString();
		}

		static long FromSubs(IEnumerable<int> subs)
		{
			var head = subs.First().Digits().Reverse();
			var tail = subs.Skip(1).Select(s => s.Digits().First());
			return head.Concat(tail).Reverse().DigitsToLong();
		}

		static IEnumerable<IEnumerable<int>> F(IEnumerable<int> d)
		{
			return F(d, Enumerable.Empty<int>(), 0);
		}

		static IEnumerable<IEnumerable<int>> F(IEnumerable<int> d, IEnumerable<int> prev, int n)
		{
			var scan = n == 0 ? 
				d.Perm(3) : 
				d.Select(x => EnumerableHelper.FromSingle(x));

			foreach (var perm in scan)
			{
				var p = prev.Skip(1).Concat(perm);
				var pn = p.Reverse().DigitsToInt();

				if (n < 7)
				{
					var nextd = d.Except(p);
					foreach (var f in F(nextd, p, n + 1))
						yield return f.Before(pn);
				}
				else
				{
					yield return EnumerableHelper.FromSingle(pn);
				}
			}
		}
	}

	public class Problem43_Another : Problem
	{
		public override string Run()
		{
			// 遅い。力技。全てのパンデジタル数を生成してチェックする。
			var digits = Enumerable.Range(0, 10).ToArray();
			var divs = new int[] { 1, 2, 3, 5, 7, 11, 13, 17 };

			var answer = digits.Perm()
				.Where(p =>
					p.First() != 0 &&
					divs
						.Select((div, i) => new { n = p.Skip(i).Take(3).Reverse().DigitsToInt(), div })
						.All(x => x.n % x.div == 0))
				.Select(p => p.Reverse().DigitsToLong())
				.Sum();

			return answer.ToString();
		}
	}

	public class Problem43_Improved : Problem
	{
		public override string Run()
		{
			var answer = Pand()
				.Select(x => x.Reverse().DigitsToLong())
				.Sum();

			return answer.ToString();
		}

		static int[] Digits = Enumerable.Range(0, 10).ToArray();
		static List<IEnumerable<IEnumerable<int>>> Patterns = GenPatterns();
		static List<IEnumerable<IEnumerable<int>>> GenPatterns()
		{
			var divs = new int[] { 1, 2, 3, 5, 7, 11, 13, 17 };
			var list = new List<long>();

			var subs = Digits.Perm(3).ToArray();
			var patterns = new List<IEnumerable<IEnumerable<int>>>();

			foreach (var div in divs)
			{
				var set = new HashSet<IEnumerable<int>>();

				foreach (var sub in subs)
				{
					var subn = sub.Reverse().DigitsToInt();
					if (subn % div == 0)
						set.Add(sub);
				}

				patterns.Add(set);
			}

			return patterns;
		}

		static IEnumerable<IEnumerable<int>> Pand()
		{
			return Pand(Digits, 0, Enumerable.Empty<int>());
		}

		static IEnumerable<IEnumerable<int>> Pand(IEnumerable<int> digits, int i, IEnumerable<int> prev)
		{
			IEnumerable<IEnumerable<int>> currents;
			if (i == 0)
			{
				currents = Patterns[i];
			}
			else
			{
				currents = Patterns[i]
					.Where(p =>
						prev.Skip(1).SequenceEqual(p.Take(2)) &&
						digits.Contains(p.Last())
						);
			}

			foreach (var c in currents)
			{
				if (i < Patterns.Count - 1)
				{
					foreach (var f in Pand(digits.Except(c), i + 1, c))
					{
						if (i == 0)
							yield return c.Concat(f);
						else
							yield return f.Before(c.Last());
					}
				}
				else
				{
					yield return EnumerableHelper.FromSingle(c.Last());
				}
			}
		}
	}
}
