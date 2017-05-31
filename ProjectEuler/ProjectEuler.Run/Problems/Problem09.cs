using System;

namespace ProjectEuler.Run.Problems
{
	/// <summary>
	/// a + b + c = 1000となるピタゴラスの三つ組が一つだけ存在する. このa,b,cの積を計算する。
	/// </summary>
	public class Problem09 : Problem
	{
		public override string Run()
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
						return answer.ToString();
						//Console.WriteLine("a = {0}, b = {1}, c = {2}", a, b, c);
					}
				}
			}

			throw new InvalidProgramException();
		}
	}
}
