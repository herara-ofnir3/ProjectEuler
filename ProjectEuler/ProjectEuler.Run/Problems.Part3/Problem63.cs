using ProjectEuler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace ProjectEuler.Run.Problems.Part3
{
	/// <summary>
	/// Problem 63 「べき乗の桁の個数」
	/// 5桁の数 16807 = 7^5は自然数を5乗した数である. 同様に9桁の数 134217728 = 8^9も自然数を9乗した数である.
	/// 
	/// 自然数を n 乗して得られる n 桁の正整数は何個あるか?
	/// </summary>
	public class Problem63 : Problem
	{
		public override string Run()
		{
			var answer = Numbers()
				.TakeWhile(n => n <= Pow(9, n).Digits().Count())
				.SelectMany(n =>
					Enumerable.Range(1, 9)
						.Select(i => Pow(i, n))
						.Where(p => p.Digits().Count() == n))
				.Count();

			return answer.ToString();
		}

		static IEnumerable<BigInteger> Numbers()
		{
			for (var n = 1; ; n++)
				yield return n;
		}

		static BigInteger Pow(BigInteger b, BigInteger e)
		{
			BigInteger r = b;
			for (BigInteger i = 1; i < e; i++)
				r *= b;
			return r;
		}
	}
}
