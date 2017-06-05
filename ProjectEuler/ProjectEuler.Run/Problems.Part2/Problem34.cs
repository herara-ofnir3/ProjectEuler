using ProjectEuler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler.Run.Problems.Part2
{
	/// <summary>
	/// Problem 34 「桁の階乗」
	/// 145は面白い数である. 1! + 4! + 5! = 1 + 24 + 120 = 145となる.
	/// 
	/// 各桁の数の階乗の和が自分自身と一致するような数の和を求めよ.
	/// 
	/// 注: 1! = 1 と 2! = 2 は総和に含めてはならない.
	/// </summary>
	public class Problem34 : Problem
	{
		public override string Run()
		{
			var max = Factorial(9) * 7;
			var list = new List<int>();

			for (var n = 3; n <= max; n++)
			{
				var sum = n
					.Digits()
					.Select(x => Factorial(x))
					.Sum();

				if (n == sum)
					list.Add(n);
			}

			var answer = list.Sum();
			return answer.ToString();
		}

		static int Factorial(int n)
		{
			if (n == 0) return 1;
			return Enumerable.Range(1, n).Aggregate((a, b) => a * b);
		}
	}
}
