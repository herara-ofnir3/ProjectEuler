using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Run.Problems
{
	/// <summary>
	/// 600851475143 の素因数のうち最大のものを求める。
	/// </summary>
	public class Problem03 : Problem
	{
		public override string Run()
		{
			var n = 600851475143;

			// まず約数を求め、その中から素数を見つけることで素因数を獲得します。
			var answer = n.Divisors().Where(i => i.IsPrime()).Max();

			return answer.ToString();
		}
	}

	public static class NumberHelper
	{
		public static IEnumerable<long> Divisors(this long n)
		{
			// 1から順に割り切れる数を探します。
			// 割り切れた場合、nをその数で割ったものも約数になります。
			// 「割り切れた場合の割った数」を [小さい約数]、
			// 「nを小さい約数で割った数」を [大きい約数] として記録します。

			// 最後に登場した [大きい約数] より大きな約数が現れることは無いので、
			// ループの条件は [最後に現れた大きい約数よりカウンタが小さい間] です。
			long minor = 1;
			long greater = n;
			var counter = minor;

			while (counter < greater)
			{
				if (n % counter == 0)
				{
					// 小さい約数と大きい約数を求めます。
					minor = counter;
					greater = n / minor;

					yield return minor;
					yield return greater;
				}

				counter++;
			}
		}

		public static bool IsPrime(this long n)
		{
			if (n < 2) return false;
			if (n == 2) return true;
			if (n % 2 == 0) return false;

			for (var i = 3; i * i < n; i += 2)
			{
				if (n % i == 0) return false;
			}

			return true;
		}
	}
}
