using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace ProjectEuler.Core
{
	public static class NumberHelper
	{
		public static IEnumerable<int> Divisors(this int n)
		{
			return Divisors((long)n).Select(i => (int)i);
		}

		public static IEnumerable<long> Divisors(this long n)
		{
			// 1から順に割り切れる数を探します。
			// 割り切れた場合、nをその数で割ったものも約数になります。
			// 「割り切れた場合の割った数」を [小さい約数]、
			// 「nを小さい約数で割った数」を [大きい約数] として記録します。

			// 最後に登場した [大きい約数] より大きな約数が現れることは無いので、
			// ループの条件は [最後に現れた大きい約数よりカウンタが小さい間] です。
			long minorDivisor = 1;
			long greaterDivisor = n;
			var counter = minorDivisor;

			while (counter < greaterDivisor)
			{
				if (n % counter == 0)
				{
					// 小さい約数と大きい約数を求めます。
					minorDivisor = counter;
					greaterDivisor = n / minorDivisor;

					// それぞれの約数を返します。
					yield return minorDivisor;
					yield return greaterDivisor;
				}

				counter++;
			}
		}

		public static IEnumerable<int> Digits(this int n, int b = 10)
		{
			if (n == 0)
				yield return 0;

			var q = n;
			while (q != 0)
			{
				yield return q % b;
				q /= b;
			}
		}

		public static IEnumerable<long> Digits(this long n, int b = 10)
		{
			if (n == 0)
				yield return 0;

			var q = n;
			while (q != 0)
			{
				yield return q % b;
				q /= b;
			}
		}

		public static IEnumerable<BigInteger> Digits(this BigInteger n, int b = 10)
		{
			if (n == 0)
				yield return 0;

			var q = n;
			while (q != 0)
			{
				yield return q % b;
				q = q / b;
			}
		}

	}
}
