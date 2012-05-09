using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler.Problem5
{
	class Program
	{
		/// <summary>
		/// 1 から 20 までの整数全てで割り切れる数字の中で最小の値(最小公倍数)を求める。
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			int n = 20;

			int multiple = n;
			int counter = multiple;

			var numbers = Enumerable.Range(1, n);

			// 大きい数から順に最小公倍数を求めていきます。
			// 現在の最小公倍数をカウントアップしていき、
			// そのときの整数で割り切れたらそれが新しい最小公倍数です。
			foreach (var i in numbers.Reverse())
			{
				while (counter % i != 0)
				{
					counter += multiple;
				} 

				multiple = counter;
			}

			Console.WriteLine(multiple);
			Console.ReadLine();
		}
	}
}
