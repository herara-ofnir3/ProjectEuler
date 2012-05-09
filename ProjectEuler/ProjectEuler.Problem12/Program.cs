using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectEuler.Core;

namespace ProjectEuler.Problem12
{
	class Program
	{
		/// <summary>
		/// 501 個以上の約数をもつ最初の三角数を求める。
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
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

			Console.WriteLine(triangular);
			Console.ReadLine();
		}
	}
}
