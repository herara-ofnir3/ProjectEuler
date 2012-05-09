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
			int answer = 0;

			int previous = 1;
			int current = 2;
			int next = 0;

			do
			{
				if (current % 2 == 0)
					answer += current;

				next = current + previous;
				previous = current;
				current = next;

			} while (current < n);

			Console.WriteLine(answer);
			Console.ReadLine();
		}
	}
}
