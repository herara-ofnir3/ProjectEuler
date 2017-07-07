using ProjectEuler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler.Run.Problems.Part4
{
	/// <summary>
	/// Problem 76 「和の数え上げ」
	/// 5は数の和として6通りに書くことができる:
	/// 
	/// 4 + 1
	/// 3 + 2
	/// 3 + 1 + 1
	/// 2 + 2 + 1
	/// 2 + 1 + 1 + 1
	/// 1 + 1 + 1 + 1 + 1
	/// 
	/// 2つ以上の正整数の和としての100の表し方は何通りか.
	/// </summary>
	public class Problem76 : Problem
	{
		public override string Run()
		{
			var n1 = F(1);
			var n2 = F(2);
			var n3 = F(3);
			var n4 = F(4);
			var n5 = F(5);
			var n6 = F(6);
			var n7 = F(7);
			var n8 = F(8);
			var n9 = F(9);
			var n10 = F(10);

			var n7i1 = F(6, 1);
			var n7i2 = F(5, 2);
			var n7i3 = F(4, 3);
			var n7i4 = F(3, 4);
			var n7i5 = F(2, 5);
			var n7i6 = F(1, 6);

			var answer = F(100);
			return answer.ToString();
		}

		static int F(int n)
		{
			if (n <= 1) return 0;
			if (n == 2) return 1;
			return Enumerable.Range(1, n - 1).Sum(m => 1 + F(n - m, m));
		}

		static int F(int n, int m)
		{
			if (m == 1) return 0;
			var max = m;
			if (n < m) max = n;

			var factors = Enumerable.Repeat(max, n / max).After(n % max).Concat(Enumerable.Range(1, max - 1).Skip(1));

			//var factors = Enumerable.Range(1, max).Skip(1)
			//	.SelectMany(i => Enumerable.Repeat(i, n / i).After(n % i));

			return factors.Select(f => F(f)).Sum();
		}
	}
}
