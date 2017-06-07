using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler.Run.Problems.Part2
{
	/// <summary>
	/// Problem 41 「パンデジタル素数」
	/// n桁パンデジタルであるとは, 1からnまでの数を各桁に1つずつ持つこととする.
	/// 
	/// #下のリンク先にあるような数学的定義とは異なる
	/// 
	/// 例えば2143は4桁パンデジタル数であり, かつ素数である. n桁（この問題の定義では9桁以下）パンデジタルな素数の中で最大の数を答えよ.
	/// </summary>
	public class Problem41 : Problem
	{
		public override string Run()
		{
			var answer = 0;

			foreach (var n in Enumerable.Range(1, 9).Reverse())
			{
				var d = Perm(Enumerable.Range(1, n).Reverse());

				answer = d
					.Select(pd => DigitsToInt(pd))
					.OrderByDescending(p => p)
					.FirstOrDefault(p => IsPrime(p));

				if (0 < answer)
					break;
			}

			return answer.ToString();
		}

		HashSet<int> _primes = new HashSet<int> { 2, 3, 5, 7 };
		int _tried = 7;

		bool IsPrime(int n)
		{
			if (n <= _tried)
				return _primes.Contains(n);

			var tryTo = Math.Sqrt(n);

			if (tryTo <= _tried)
				return _primes.All(p => n % p != 0);

			for (var i = _primes.Last() + 2; i <= tryTo; i += 2)
			{
				_tried = i;

				if (_primes.Any(p => i % p == 0))
					continue;

				_primes.Add(i);
			}

			return _primes.All(p => n % p != 0);
		}

		static int DigitsToInt(IEnumerable<int> digits)
		{
			return digits.Select((x, i) => x * (int)Math.Pow(10, i)).Sum();
		}

		static IEnumerable<IEnumerable<T>> Perm<T>(IEnumerable<T> items)
		{
			var i = 0;
			foreach (var item in items)
			{
				var single = true;
				var without = items.Where((x, index) => index != i);

				foreach (var p in Perm(without))
				{
					yield return Composite(item, p);
					single = false;
				}

				if (single)
					yield return Single(item);

				i++;
			}
		}

		static IEnumerable<T> Single<T>(T item) { yield return item; }

		static IEnumerable<T> Composite<T>(T head, IEnumerable<T> tail)
		{
			yield return head;

			foreach (var i in tail)
				yield return i;
		}
	}
}
