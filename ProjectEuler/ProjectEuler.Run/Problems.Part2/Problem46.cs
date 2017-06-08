using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler.Run.Problems.Part2
{
	/// <summary>
	/// Problem 46 「もうひとつのゴールドバッハの予想」
	/// Christian Goldbachは全ての奇合成数は平方数の2倍と素数の和で表せると予想した.
	/// 
	/// 9 = 7 + 2×1^2
	/// 15 = 7 + 2×2^2
	/// 21 = 3 + 2×3^2
	/// 25 = 7 + 2×3^2
	/// 27 = 19 + 2×2^2
	/// 33 = 31 + 2×1^2
	/// 
	/// 後に, この予想は誤りであることが分かった.
	/// 
	/// 平方数の2倍と素数の和で表せない最小の奇合成数はいくつか?
	/// </summary>
	public class Problem46 : Problem
	{
		public override string Run()
		{
			var answer = 0;
			foreach (var c in GenCom())
			{
				var correct = false;
				var p = 0;
				for (var n = 1; p < c ; n++)
				{
					p = c - 2 * (int)Math.Pow(n, 2);

					if (IsPrime(p))
					{
						correct = true;
						break;
					}
				}

				if (!correct)
				{
					answer = c;
					break;
				}
			}

			return answer.ToString();
		}

		List<int> _coms = new List<int> { 9 };

		IEnumerable<int> GenCom()
		{
			var last = 0;

			foreach (var c in _coms)
			{
				yield return c;
				last = c;
			}

			for (var n = last + 2; ; n += 2)
			{
				if (IsPrime(n))
					continue;

				yield return n;
				_coms.Add(n);
			}
		}

		List<int> _primes = new List<int> { 2, 3, 5, 7 };

		bool IsPrime(int num)
		{
			var last = _primes[_primes.Count - 1];
			if (num <= last)
				return _primes.Contains(num);

			var max = Math.Sqrt(num);
			for (var n = last + 2; n <= max; n += 2)
			{
				if (_primes.Any(p => n % p == 0))
					continue;

				_primes.Add(n);
			}

			return _primes
				.TakeWhile(p => p <= max)
				.All(p => num % p != 0);
		}
	}
}
