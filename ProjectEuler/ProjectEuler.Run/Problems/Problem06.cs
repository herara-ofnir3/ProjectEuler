using System.Linq;

namespace ProjectEuler.Run.Problems
{
	/// <summary>
	/// 最初の100個の自然数の和の二乗と二乗の和の差を求める。
	/// </summary>
	public class Problem06 : Problem
	{
		public override string Run()
		{
			int n = 100;
			var numbers = Enumerable.Range(1, n);

			var sum = numbers.Sum();
			var sumSquare = sum * sum; // 和の二乗を求めます。
			var squreSum = numbers.Sum(i => i * i); // 二乗数の和を求めます。
			var answer = sumSquare - squreSum;

			return answer.ToString();
		}
	}
}
