using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler.Problem2
{
	class Program
	{
		/// <summary>
		/// フィボナッチ数列(最初の二項は 1, 2)の項の値が400万を超えない範囲で、偶数値の項の総和を求める。
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			int n = 4000000;
			var answer = FibonacciNumbers(n - 1)
				.Where(i => i%2 == 0)
				.Sum();

			Console.WriteLine(answer);
			Console.ReadLine();
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
