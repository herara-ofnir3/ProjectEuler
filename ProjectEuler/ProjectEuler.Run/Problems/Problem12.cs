using ProjectEuler.Core;
using System.Linq;

namespace ProjectEuler.Run.Problems
{
	/// <summary>
	/// 501 個以上の約数をもつ最初の三角数を求める。
	/// </summary>
	public class Problem12 : Problem
	{
		public override string Run()
		{
			int n = 500;
			int triangular = 0;
			var divisors = Enumerable.Empty<int>();
			int i = 1;

			while (divisors.Count() < n)
			{
				triangular += i;
				divisors = triangular.Divisors();
				i++;
			}

			return triangular.ToString();
		}
	}
}
