using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler.Problem9
{
	class Program
	{
		/// <summary>
		/// a + b + c = 1000となるピタゴラスの三つ組が一つだけ存在する. このa,b,cの積を計算する。
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			// a^2 + b^2 = c^2

			// a + b + c = 1000 であるとき c は、
			// c = 1000 - a - b となる。
			// また、[a < b < c] なので、a の探索は b よりも小さい数で行う。

			int c = 0;
			int left = 0;
			int right = 0;

			for (int b = 1000; b > 0; b--)
			{
				for (int a = b; a > 0; a--)
				{
					c = 1000 - a - b;

					left = (a * a) + (b * b);
					right = c * c;

					if (left == right)
					{
						int answer = a * b * c;
						Console.WriteLine(answer);
						Console.WriteLine("a = {0}, b = {1}, c = {2}", a, b, c);
						Console.ReadLine();
					}
				}
			}
		}
	}
}
