using ProjectEuler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace ProjectEuler.Run.Problems
{
	/// <summary>
	/// 各位の数字の和 2
	/// 
	/// n × (n - 1) × ... × 3 × 2 × 1 を n! と表す.
	///
	/// 例えば, 10! = 10 × 9 × ... × 3 × 2 × 1 = 3628800 となる.
	/// この数の各桁の合計は 3 + 6 + 2 + 8 + 8 + 0 + 0 = 27 である.
	///
	/// では, 100! の各位の数字の和を求めよ.
	/// </summary>
	public class Problem20 : Problem
	{
		public override string Run()
		{
			var n = 100;
			var f = Factorial(n);
			var answer = f.Digits().Sum(x => (int)x);
			return answer.ToString();
		}

		static BigInteger Factorial(int n)
		{
			return Enumerable.Range(1, n)
				.Select(x => (BigInteger)x)
				.Aggregate((l, r) => l * r);
		}
	}

	public class Problem20_Another : Problem
	{
		public override string Run()
		{
			var n = 100;
			var f = Factorial(n);
			var answer = f.Digits().Sum();
			return answer.ToString();
		}

		static BigNumber Factorial(int n)
		{
			return Enumerable.Range(1, n)
				.Select(x => new BigNumber(x.Digits()))
				.Aggregate((l, r) => l * r);
		}


		private class BigNumber
		{
			public BigNumber(IEnumerable<int> digits)
			{
				_digits = digits.ToList();
			}

			private IList<int> _digits;

			public IEnumerable<int> Digits()
			{
				return _digits.ToArray();
			}

			public static BigNumber operator+(BigNumber left, BigNumber right)
			{
				var result = new List<int>();

				var ld = left._digits.ToList();
				var rd = right._digits.ToList();

				var longer = ld;
				if (ld.Count < rd.Count)
					longer = rd;

				var carry = 0;
				for (var i = 0; i < longer.Count; i++)
				{
					var l = ld.ElementAtOrDefault(i);
					var r = rd.ElementAtOrDefault(i);
					var sum = l + r + carry;
					result.Add(sum % 10);
					carry = sum / 10;
				}

				if (0 < carry)
					result.Add(carry);

				return new BigNumber(result);
			}

			public static BigNumber operator*(BigNumber left, BigNumber right)
			{
				var sums = new List<BigNumber>();

				foreach (var r in right._digits.Select((x, i) => new { x, i }))
				{
					var t = new List<int>();
					var carry = 0;
					foreach (var l in left._digits.Select((x, i) => new { x, i }))
					{
						var multi = l.x * r.x + carry;
						t.Add(multi % 10);
						carry = multi / 10;
					}

					if (0 < carry)
						t.Add(carry);

					for (var i = 0; i < r.i; i++)
						t.Insert(0, 0);

					sums.Add(new BigNumber(t));
				}

				return sums.Aggregate((l, r) => l + r);
			}

			public override string ToString()
			{
				return string.Join("", _digits.Reverse());
			}

			public static BigNumber Parse(string value)
			{
				return new BigNumber(value.Select(c => int.Parse(c.ToString())).Reverse());
			}
		}
	}
}
