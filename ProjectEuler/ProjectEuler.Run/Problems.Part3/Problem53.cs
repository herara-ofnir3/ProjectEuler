using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler.Run.Problems.Part3
{
	/// <summary>
	/// Problem 53 「組み合わせ選択」
	/// 12345から3つ選ぶ選び方は10通りである.
	/// 
	/// 123, 124, 125, 134, 135, 145, 234, 235, 245, 345.
	/// 組み合わせでは, 以下の記法を用いてこのことを表す: 5C3 = 10.
	/// 
	/// 一般に, r ≤ n について nCr = n!/(r!(n-r)!) である. ここで, n! = n×(n−1)×...×3×2×1, 0! = 1 と階乗を定義する.
	/// 
	/// n = 23 になるまで, これらの値が100万を超えることはない: 23C10 = 1144066.
	/// 
	/// 1 ≤ n ≤ 100 について, 100万を超える nCr は何通りあるか?
	/// </summary>
	public class Problem53 : Problem
	{
		public override string Run()
		{
			var range = Enumerable.Range(1, 100);
			var patterns = (from n in range
						   from r in range.Take(n)
						   select new { n, r }).SkipWhile(p => p.n < 23);

			var answer = patterns.Count(p => 1000000 < C(p.n, p.r));
			return answer.ToString();
		}

		static BigInteger C(int n, int r)
		{
			return Factorial(n) / (Factorial(r) * Factorial(n - r));
		}

		static BigInteger Factorial(int n)
		{
			if (n == 0) return 1;
			return Enumerable.Range(1, n)
				.Select(x => (BigInteger)x)
				.Aggregate((a, b) => a * b);
		}
	}
}
