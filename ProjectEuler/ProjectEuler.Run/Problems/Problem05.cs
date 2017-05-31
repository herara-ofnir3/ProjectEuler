using System.Linq;

namespace ProjectEuler.Run.Problems
{
	/// <summary>
	/// 1 から 20 までの整数全てで割り切れる数字の中で最小の値(最小公倍数)を求める。
	/// </summary>
	public class Problem05 : Problem
	{
		public override string Run()
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

			return multiple.ToString();
		}
	}
}
