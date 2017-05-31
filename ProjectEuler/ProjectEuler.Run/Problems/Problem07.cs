using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Run.Problems
{
	/// <summary>
	/// 10001 番目の素数を求める。
	/// </summary>
	public class Problem07 : Problem
	{
		public override string Run()
		{
			int n = 10001;

			// 数をカウントアップしていき、その時の素数リスト内の数値で割り切れるかを確認します。
			// もし全ての素数で割り切れなかった場合は、素数なので素数リストに加えます。
			// 素数リストの数が、指定の数になるまで続けます。
			var firstPrimer = 2;
			var primers = new List<int>() { };
			var counter = firstPrimer;

			while (primers.Count < n)
			{
				bool isPrime = (!primers.Any(prime => counter % prime == 0));

				if (isPrime)
				{
					primers.Add(counter);
				}

				counter++;
			}

			var answer = primers.Last();
			return answer.ToString();
		}
	}
}
