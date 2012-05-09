using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler.Problem1
{
	class Program
	{
		/// <summary>
		/// 1,000 未満の 3 か 5 の倍数になっている数字の合計を求める。
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			int n = 1000;

			var answer = Enumerable
				.Range(1, n - 1)
				.Where(i => i % 3 == 0 || i % 5 == 0)
				.Sum();

			Console.WriteLine(answer);
			Console.ReadLine();
		}
	}
}
