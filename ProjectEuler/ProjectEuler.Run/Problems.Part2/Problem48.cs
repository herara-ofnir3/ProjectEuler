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
	/// Problem 48 「自身のべき乗(self powers)」
	/// 次の式は, 1^1 + 2^2 + 3^3 + ... + 10^10 = 10405071317 である.
	/// 
	/// では, 1^1 + 2^2 + 3^3 + ... + 1000^1000 の最後の10桁を求めよ.
	/// </summary>
	public class Problem48 : Problem
	{
		public override string Run()
		{
			var answer = Enumerable.Range(1, 1000)
				.Select(x => Pow(x, x))
				.Aggregate((a, b) => a + b) % 10000000000;

			return answer.ToString();
		}

		static BigInteger Pow(int b, int e) => 
			Enumerable.Range(1, e)
				.Select(_ => (BigInteger)b)
				.Aggregate((l, r) => l * r);
	}
}
