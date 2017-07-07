using ProjectEuler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler.Run.Problems.Part3
{
	/// <summary>
	/// Problem 66 「ディオファントス方程式」
	/// 次の形式の, 2次のディオファントス方程式を考えよう:
	/// 
	/// x^2 - Dy^2 = 1
	/// たとえば D=13 のとき, x を最小にする解は 649^2 - 13×180^2 = 1 である.
	/// 
	/// D が平方数(square)のとき, 正整数のなかに解は存在しないと考えられる.
	/// 
	/// D = {2, 3, 5, 6, 7} に対して x を最小にする解は次のようになる:
	/// 
	/// 3^2 - 2×2^2 = 1
	/// 2^2 - 3×1^2 = 1
	/// 9^2 - 5×4^2 = 1
	/// 5^2 - 6×2^2 = 1
	/// 8^2 - 7×3^2 = 1
	/// したがって, D ≤ 7 に対して x を最小にする解を考えると, D=5 のとき x は最大である.
	/// 
	/// D ≤ 1000 に対する x を最小にする解で, x が最大になるような D の値を見つけよ.
	/// </summary>
	public class Problem66 : Problem
	{
		public override string Run()
		{
			// Dの平方根の連分数展開で (x, y) の解が求まるらしい。
			// 理屈はよくわからないが確かに動く。
			var dmax = 1000;
			var answer = Numbers()
				.Skip(1)
				.Where(n => !Squares().TakeWhile(s => s <= n).Contains(n))
				.TakeWhile(d => d <= dmax)
				.Select(d => new { D = d, x = Dx(d) })
				.OrderByDescending(d => d.x)
				.First();

			return answer.ToString();
		}

		static BigInteger Dx(int d)
		{
			var cf = CF(d); // Dの平方根を連分数展開する
			var m = cf.Length - 1; // 連分数展開の周期mを求める。

			// Dの平方根の近似値eを求める。
			// このとき連分数展開の周期m-1番目の近似値を取得する。
			// なお、mが1のときは1番め。
			var e = E(d, m == 1 ? 1 : m - 1); 

			// 近似値 e = (x / y) の x, y がそのまま解となる。
			// ただし、周期mが奇数の場合は x^2 - y^2 * D = -1 の解(右辺が-1の場合の解)となるので、
			// xk = yk * √D = (x + y√D)^k を用いて、右辺が1の解を求める。
			if (1 < m && m % 2 != 0)
			{
				var x1 = e.num;
				var y1 = e.denom;
				var z = (BigInteger)(((long)x1 + (long)y1 * Math.Sqrt(d)) * ((long)x1 + (long)y1 * Math.Sqrt(d)));
				return z / 2;
			}

			return e.num;
		}

		static Dictionary<int, int[]> CFMap = new Dictionary<int, int[]>();

		static int[] CF(int d)
		{
			if (!CFMap.ContainsKey(d))
			{
				var cf = ExpansionSqrtToContinuedFraction(d);
				CFMap[d] = RepeatPart(cf.Skip(1))
					.Before(cf.First())
					.Select(x => x.i)
					.ToArray();
			}

			return CFMap[d];
		}

		static int A(int d, int n)
		{
			var a = CF(d);
			if (n == 0)
				return a[0];

			if (a.Length == 2)
				return a[1];

			return a[n % (a.Length - 1)];
		}

		static (BigInteger num, BigInteger denom) E(int d, int n)
		{
			return E(d, n - 1, (1, A(d, n)));
		}

		static (BigInteger BigInteger, BigInteger denom) E(int d, int n, (BigInteger num, BigInteger denom) f)
		{
			if (n == 0)
				return Add(A(d, 0), f);

			return E(d, n - 1, Div(1, Add(A(d, n), f)));
		}

		static (BigInteger num, BigInteger denom) Add(BigInteger n, (BigInteger num, BigInteger denom) f)
		{
			return (n * f.denom + f.num, f.denom);
		}

		static (BigInteger num, BigInteger denom) Div(BigInteger l, (BigInteger num, BigInteger denom) r)
		{
			return (l * r.denom, r.num);
		}

		static IEnumerable<T> RepeatPart<T>(IEnumerable<T> items)
		{
			var head = items.First();
			var tail = items.Skip(1);
			return tail.TakeWhile(x => !x.Equals(head)).Before(head);
		}

		static IEnumerable<(int i, FractionalPart f)> ExpansionSqrtToContinuedFraction(int n)
		{
			var a0 = SqrtToFraction(n);
			yield return a0;

			foreach (var an in CF(a0.f))
				yield return an;
		}

		static IEnumerable<(int ip, FractionalPart fp)> CF(FractionalPart fp)
		{
			var ipfp = F(fp);
			if (ipfp.i == 0)
				yield break;

			yield return ipfp;

			foreach (var f in CF(ipfp.f))
				yield return f;
		}

		static (int i, FractionalPart f) F(FractionalPart fp)
		{
			var denom = fp.SqrtA - fp.Adjust * fp.Adjust;
			if (denom == 0)
				return (0, fp);

			var i = (int)(fp.Num * (Math.Sqrt(fp.SqrtA) + fp.Adjust) / denom);
			var c = denom / fp.Num;
			return (i, new FractionalPart(fp.SqrtA, Math.Abs(fp.Adjust - i * c), c));
		}

		static (int i, FractionalPart f) SqrtToFraction(int n)
		{
			var i = (int)Math.Sqrt(n);
			var f = new FractionalPart(n, i, 1);
			return (i, f);
		}

		struct FractionalPart
		{
			public FractionalPart(int sqrtA, int adjust, int num)
			{
				_inner = (sqrtA, adjust, num);
			}

			readonly (int sqrtA, int adjust, int num) _inner;
			public int SqrtA => _inner.sqrtA;
			public int Adjust => _inner.adjust;
			public int Num => _inner.num;

			public override bool Equals(object obj)
			{
				if (!(obj is FractionalPart)) return false;
				var other = (FractionalPart)obj;
				return _inner.Equals(other._inner);
			}

			public override int GetHashCode()
			{
				return _inner.GetHashCode();
			}

			public override string ToString()
			{
				return $"{Num} / (√{SqrtA} - {Adjust})";
			}
		}

		static IEnumerable<int> Squares() => Numbers().Select(n => n * n);

		static IEnumerable<int> Numbers()
		{
			for (var n = 1; ; n++) yield return n;
		}
	}
}
