using ProjectEuler.Core;
using System.Linq;

namespace ProjectEuler.Run.Problems.Part2
{
	/// <summary>
	/// Problem 36 「二種類の基数による回文数」
	/// 585 = 10010010012 (2進) は10進でも2進でも回文数である.
	/// 
	/// 100万未満で10進でも2進でも回文数になるような数の総和を求めよ.
	/// 
	/// (注: 先頭に0を含めて回文にすることは許されない.)
	/// </summary>
	public class Problem36 : Problem
	{
		public override string Run()
		{
			var answer = Enumerable.Range(1, 1000000 - 1)
				.Where(n => Palindrome(n, 10) && Palindrome(n, 2))
				.Sum();

			return answer.ToString();
		}

		static bool Palindrome(int n, int b)
		{
			var d = n.Digits(b);
			return d.SequenceEqual(d.Reverse());
		}
	}
}
