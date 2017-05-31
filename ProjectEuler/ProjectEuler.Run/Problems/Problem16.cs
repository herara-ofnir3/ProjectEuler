using ProjectEuler.Core;
using System;
using System.Linq;

namespace ProjectEuler.Run.Problems
{
	/// <summary>
	/// 各位の数字の和
	/// 
	/// 2^15 = 32768 であり, 各位の数字の和は 3 + 2 + 7 + 6 + 8 = 26 となる.
	/// 同様にして, 2^1000 の各位の数字の和を求めよ.
	/// </summary>
	public class Problem16 : Problem
	{
		public override string Run()
		{
			var n = (long)Math.Pow(2, 15);
			var answer = n.Digits().Sum();
			return answer.ToString();
		}
	}
}
