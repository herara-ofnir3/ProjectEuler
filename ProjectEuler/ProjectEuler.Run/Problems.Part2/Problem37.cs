using ProjectEuler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler.Run.Problems.Part2
{
	/// <summary>
	/// Problem 37 「切り詰め可能素数」
	/// 3797は面白い性質を持っている. まずそれ自身が素数であり, 左から右に桁を除いたときに全て素数になっている (3797, 797, 97, 7). 同様に右から左に桁を除いたときも全て素数である (3797, 379, 37, 3).
	/// 
	/// 右から切り詰めても左から切り詰めても素数になるような素数は11個しかない. 総和を求めよ.
	/// 
	/// 注: 2, 3, 5, 7を切り詰め可能な素数とは考えない.
	/// </summary>
	public class Problem37 : Problem
	{
		public override string Run()
		{
			var answer = Primes()
				.Where(p => Truncatable(p))
				.Take(11)
				.Sum();

			return answer.ToString();
		}

		bool Truncatable(int p)
		{
			var d = p.Digits();

			if (d.Count() == 1)
				return false;

			var leftTo = Enumerable.Range(1, d.Count() - 1)
				.Select(n => DigitsToInt(d.Skip(n)));

			var rightTo = Enumerable.Range(1, d.Count() - 1)
				.Select(n => DigitsToInt(d.Take(n)));

			return leftTo.Concat(rightTo).All(n => IsPrime(n));
		}

		HashSet<int> _primes = new HashSet<int> { 2, 3 };

		IEnumerable<int> Primes()
		{
			var n = 0;

			foreach (var p in _primes)
			{
				yield return p;
				_primes.Add(p);
			}

			n = _primes.Last() + 2;

			for (;;n+=2)
			{
				if (_primes.Any(p => n % p == 0))
					continue;

				yield return n;
				_primes.Add(n);
			}
		}

		bool IsPrime(int n) => _primes.Contains(n);

		static int DigitsToInt(IEnumerable<int> digits)
		{
			return digits.Select((x, i) => x * (int)Math.Pow(10, i)).Sum();
		}
	}
}
