using ProjectEuler.Core;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Run.Problems
{
	/// <summary>
	/// 友愛数
	/// 
	/// d(n) を n の真の約数の和と定義する. (真の約数とは n 以外の約数のことである. )
	/// もし, d(a) = b かつ d(b) = a (a ≠ b のとき) を満たすとき, a と b は友愛数(親和数)であるという.
	///
	/// 例えば, 220 の約数は 1, 2, 4, 5, 10, 11, 20, 22, 44, 55, 110 なので d(220) = 284 である.
	/// また, 284 の約数は 1, 2, 4, 71, 142 なので d(284) = 220 である.
	///
	/// それでは10000未満の友愛数の和を求めよ.	
	/// </summary>
	public class Problem21 : Problem
	{
		public override string Run()
		{
			var an = new List<int>();

			for (var n = 1; n < 10000; n++)
			{
				var o = D(n);
				if (n == D(o) && n != o)
				{
					an.Add(n);
				}
			}

			var answer = an.Sum();
			return answer.ToString();
		}

		static int D(int n)
		{
			return n.TrueDivisors().Sum();
		}
	}
}
