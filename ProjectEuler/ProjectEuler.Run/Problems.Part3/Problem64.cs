using ProjectEuler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler.Run.Problems.Part3
{
	/// <summary>
	/// Problem 64 「奇数周期の平方根」
	/// 平方根は連分数の形で表したときに周期的であり, 以下の形で書ける:
	/// 
	/// √N = a0 + 1 / (a1 + 1 / (a2 + 1 / (a3 + ...)))
	/// 
	/// 例えば, √23を考えよう.
	/// 
	/// √23 = 4 + √23 - 4 = 4 + 1 / (1 / (√23 - 4)) = 4 + 1 / (1 + (√23 - 3) / 7)
	/// 
	/// となる.
	/// 
	/// この操作を続けていくと,
	/// 
	/// √23 = 4 + 1 / (1 + 1 / (3 + 1 / (1 + 1 / (8 + ...))))
	/// 
	/// を得る.
	/// 
	/// 操作を纏めると以下になる:
	/// 
	/// a0 = 4, 1/(√23-4) = (√23+4)/7 = 1 + (√23-3)/7
	/// a1 = 1, 7/(√23-3) = 7(√23+3)/14 = 3 + (√23-3)/2
	/// a2 = 3, 2/(√23-3) = 2(√23+3)/14 = 1 + (√23-4)/7
	/// a3 = 1, 7/(√23-4) = 7(√23+4)/7 = 8 + (√23-4)
	/// a4 = 8, 1/(√23-4) = (√23+4)/7 = 1 + (√23-3)/7
	/// a5 = 1, 7/(√23-3) = 7(√23+3)/14 = 3 + (√23-3)/2
	/// a6 = 3, 2/(√23-3) = 2(√23+3)/14 = 1 + (√23-4)/7
	/// a7 = 1, 7/(√23-4) = 7(√23+4)/7 = 8 + (√23-4)
	/// よって, この操作は繰り返しになることが分かる. 表記を簡潔にするために, √23 = [4;(1,3,1,8)]と表す. (1,3,1,8)のブロックは無限に繰り返される項を表している.
	/// 
	/// 最初の10個の無理数である平方根を連分数で表すと以下になる.
	/// 
	/// √2=[1;(2)], period=1
	/// √3=[1;(1,2)], period=2
	/// √5=[2;(4)], period=1
	/// √6=[2;(2,4)], period=2
	/// √7=[2;(1,1,1,4)], period=4
	/// √8=[2;(1,4)], period=2
	/// √10=[3;(6)], period=1
	/// √11=[3;(3,6)], period=2
	/// √12= [3;(2,6)], period=2
	/// √13=[3;(1,1,1,1,6)], period=5
	/// N ≤ 13で奇数の周期をもつ平方根は丁度4つある.
	/// 
	/// N ≤ 10000 について奇数の周期をもつ平方根が何個あるか答えよ.
	/// </summary>
	public class Problem64 : Problem
	{
		public override string Run()
		{
			var list = Numbers()
				.Where(n => 
					!SquareNumbers()
						.TakeWhile(sq => sq <= n)
						.Contains(n))
				.TakeWhile(n => n <= 10000)
				.ToArray();

			var answer = list.Count(n => 
				RepeatPart(ExpansionSqrtToContinuedFraction(n).Skip(1)).Count() % 2 != 0);

			return answer.ToString();
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

		static IEnumerable<(int ip, FractionalPart fp)>CF(FractionalPart fp)
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

			var d = fp.Num * (Math.Sqrt(fp.SqrtA) + fp.Adjust) / denom;
			var i = (int)d;
			var c = denom / fp.Num;
			return (i, new FractionalPart(fp.SqrtA, Math.Abs(fp.Adjust - i * c), c));
		}

		static (int i, FractionalPart f) SqrtToFraction(int n)
		{
			var i = (int)Math.Sqrt(n);
			var f = new FractionalPart(n, i, 1);
			return (i, f);
		}

		static IEnumerable<int> SquareNumbers() => Numbers().Select(n => n * n);

		static IEnumerable<int> Numbers()
		{
			for (var n = 1; ; n++) yield return n;
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
	}
}