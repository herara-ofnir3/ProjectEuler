using ProjectEuler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler.Run.Problems.Part2
{
	/// <summary>
	/// Problem 30 「各桁の5乗」 
	/// 驚くべきことに, 各桁を4乗した数の和が元の数と一致する数は3つしかない.
	/// 
	/// 1634 = 1^4 + 6^4 + 3^4 + 4^4
	/// 8208 = 8^4 + 2^4 + 0^4 + 8^4
	/// 9474 = 9^4 + 4^4 + 7^4 + 4^4
	/// ただし, 1=1^4は含まないものとする. この数たちの和は 1634 + 8208 + 9474 = 19316 である.
	/// 
	/// 各桁を5乗した数の和が元の数と一致するような数の総和を求めよ.
	/// </summary>
	public class Problem30 : Problem
	{
		public override string Run()
		{
			var ex = 5;
			var max = (int)Math.Pow(9, ex) * (ex + 1);

			var answer = Scan(max)
				.Where(n => n == F(n, ex))
				.Sum();
				
			return answer.ToString();
		}

		static IEnumerable<int> Scan(int max)
		{
			for (var n = 2; n <= max; n++)
			{
				yield return n;
			}
		}

		static int F(int n, int ex)
		{
			return n
				.Digits()
				.Select(x => (int)Math.Pow(x, ex))
				.Sum();
		}
	}
}
