using ProjectEuler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler.Run.Problems.Part2
{
	/// <summary>
	/// Problem 50 「連続する素数の和」
	/// 素数41は6つの連続する素数の和として表せる:
	/// 
	/// 41 = 2 + 3 + 5 + 7 + 11 + 13.
	/// 100未満の素数を連続する素数の和で表したときにこれが最長になる.
	/// 
	/// 同様に, 連続する素数の和で1000未満の素数を表したときに最長になるのは953で21項を持つ.
	/// 
	/// 100万未満の素数を連続する素数の和で表したときに最長になるのはどの素数か?
	/// </summary>
	public class Problem50 : Problem
	{
		public override string Run()
		{
			// 激遅くん
			var primes = Prime.Gen(1000000 - 1).ToArray();
			var answer = F(primes)
				.OrderByDescending(x => x.len)
				.First();
			return answer.ToString();
		}

		static IEnumerable<(int p, int len)> F(int[] primes)
		{
			var max = primes.Max();

			var i = 1;
			foreach (var head in primes)
			{
				var sum = head;
				var count = 1;

				foreach (var x in primes.Skip(i))
				{
					sum += x;
					count++;

					if (max < sum)
						break;

					if (primes.Contains(sum))
						yield return (sum, count);
				}
				i++;
			}
		}
	}
}
