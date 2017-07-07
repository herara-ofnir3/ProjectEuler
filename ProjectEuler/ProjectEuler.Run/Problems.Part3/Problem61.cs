using ProjectEuler.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Run.Problems.Part3
{
	public class Problem61 : Problem
	{
		public override string Run()
		{
			// 三から八角数の全ての4桁の数字を列挙する。
			var triangler = GenTriangler().SkipWhile(n => n < 1000).TakeWhile(n => n <= 9999).Select(x => new Polygonal(3, x));
			var square = GenSquare().SkipWhile(n => n < 1000).TakeWhile(n => n <= 9999).Select(x => new Polygonal(4, x));
			var penta = GenPenta().SkipWhile(n => n < 1000).TakeWhile(n => n <= 9999).Select(x => new Polygonal(5, x));
			var hexa = GenHexa().SkipWhile(n => n < 1000).TakeWhile(n => n <= 9999).Select(x => new Polygonal(6, x));
			var hepta = GenHepta().SkipWhile(n => n < 1000).TakeWhile(n => n <= 9999).Select(x => new Polygonal(7, x));
			var octa = GenOcta().SkipWhile(n => n < 1000).TakeWhile(n => n <= 9999).Select(x => new Polygonal(8, x));

			var fourDigitsPolygonals = triangler
				.Concat(square)
				.Concat(penta)
				.Concat(hexa)
				.Concat(hepta)
				.Concat(octa);

			// 列挙した全ての数字から、巡回しているペアを取得する。
			var pairs = ToPairs(fourDigitsPolygonals).ToList();

			// 6回まで巡回する順列を全て列挙して、その中から最初と最後が巡回しているものを探す。
			var answer = Search(pairs, new Polygonal[] { }, 6)
				.Where(x => x.Count() == 6 && IsCyclic(x.Last().Value, x.First().Value))
				.Single() // 問題文からそのような順列は一つしかない。
				.Sum(x => x.Value);
				
			return answer.ToString();
		}

		static IEnumerable<IEnumerable<Polygonal>> Search(IEnumerable<Pair> pairs, Polygonal[] perm, int len)
		{
			if (len == 0)
			{
				yield return Enumerable.Empty<Polygonal>();
			}
			else
			{
				IEnumerable<Polygonal> nexts;
				if (perm.Length == 0)
				{
					nexts = pairs.Where(p => p.Left.P == 8).Select(p => p.Left).Distinct();
				}
				else
				{
					var last = perm.Last();
					nexts = pairs.Where(x =>
						x.Left.Equals(last) &&
						perm.All(y => y.P != x.Right.P))
						.Select(x => x.Right);
				}

				foreach (var n in nexts)
				{
					foreach (var x in Search(pairs, perm.After(n).ToArray(), len - 1))
						yield return x.Before(n);
				}
			}
		}

		static IEnumerable<Pair> ToPairs(IEnumerable<Polygonal> polygonals)
		{
			return polygonals
				.SelectMany(p => 
					polygonals
						.Where(o => p.P != o.P && IsCyclic(p.Value, o.Value))
						.Select(o => new Pair(p, o)));
		}

		class Pair
		{
			public Pair(Polygonal left, Polygonal right)
			{
				Left = left;
				Right = right;
			}

			public Polygonal Left { get; }
			public Polygonal Right { get; }

			public override string ToString()
			{
				return $"L:({Left}), R:({Right})";
			}
		}

		struct Polygonal : IEquatable<Polygonal>
		{
			public Polygonal(int p, int value)
			{
				P = p; Value = value;
			}

			public int P { get; }
			public int Value { get; }

			public override bool Equals(object obj) => Equals((Polygonal)obj);
			public bool Equals(Polygonal other) => P == other.P && Value == other.Value;
			public override int GetHashCode() => P.GetHashCode() ^ Value.GetHashCode();
			public override string ToString() => $"P:{P}, Value:{Value}";
		}

		static bool IsCyclic(int a, int b)
		{
			return a.Digits().Reverse().Skip(2).SequenceEqual(b.Digits().Reverse().Take(2));
		}

		static int Triangler(int n) => n * (n + 1) / 2;
		static int Square(int n)	=> n * n;
		static int Penta(int n)		=> n * (3 * n - 1) / 2;
		static int Hexa(int n)		=> n * (2 * n - 1);
		static int Hepta(int n)		=> n * (5 * n - 3) / 2;
		static int Octa(int n)		=> n * (3 * n - 2);

		static IEnumerable<int> GenTriangler()	=> Numbers().Select(n => Triangler(n));
		static IEnumerable<int> GenSquare()		=> Numbers().Select(n => Square(n));
		static IEnumerable<int> GenPenta()		=> Numbers().Select(n => Penta(n));
		static IEnumerable<int> GenHexa()		=> Numbers().Select(n => Hexa(n));
		static IEnumerable<int> GenHepta()		=> Numbers().Select(n => Hepta(n));
		static IEnumerable<int> GenOcta()		=> Numbers().Select(n => Octa(n));

		static IEnumerable<int> Numbers()
		{
			for (var n = 1; ; n++)
				yield return n;
		}
	}
}
