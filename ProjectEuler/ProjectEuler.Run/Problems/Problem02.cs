using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Run.Problems
{
	/// <summary>
	/// フィボナッチ数列(最初の二項は 1, 2)の項の値が400万を超えない範囲で、偶数値の項の総和を求める。
	/// </summary>
	public class Problem02 : Problem
	{
		public override string Run()
		{
			int n = 4000000;
			var answer = FibonacciNumbers(n - 1)
				.Where(i => i % 2 == 0)
				.Sum();

			return answer.ToString();
		}

		static IEnumerable<int> FibonacciNumbers(int max)
		{
			foreach (var i in FibonacciNumbers())
			{
				if (max < i)
					yield break;

				yield return i;
			}
		}

		static IEnumerable<int> FibonacciNumbers()
		{
			var previous = 0;
			var current = 1;
			var next = 0;

			while (true)
			{
				next = previous + current;
				yield return next;

				previous = current;
				current = next;
			}
		}
	}
}
