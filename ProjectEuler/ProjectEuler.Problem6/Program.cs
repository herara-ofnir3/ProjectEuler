using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler.Problem6
{
	class Program
	{
		/// <summary>
		/// 最初の100個の自然数の和の二乗と二乗の和の差を求める。
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			int n = 100;

			var numbers = Enumerable.Range(1, n);

			var sum = numbers.Sum();
			var sumSquare = sum * sum; // 和の二乗を求めます。
			var squreSum = numbers.Sum(i => i * i); // 二乗数の和を求めます。

			var answer = sumSquare - squreSum;

			Console.WriteLine(answer);
			Console.ReadLine();
		}
	}
}
