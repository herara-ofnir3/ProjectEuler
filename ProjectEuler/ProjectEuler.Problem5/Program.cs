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

			int multiple = 1;
			int counter = 1;

			var numbers = Enumerable.Range(1, n);

			foreach (var i in numbers)
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
