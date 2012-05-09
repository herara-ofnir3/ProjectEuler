using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler.Problem3
{
	class Program
	{
		/// <summary>
		/// 600851475143 の素因数のうち最大のものを求める。
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			long n = 600851475143;

			// まず約数を求め、その中から素数を見つけることで素因数を獲得します。
			var divisors = DivisorsFor(n);
			var primeFactors = FindPrimers(divisors);

			var answer = primeFactors.Max();
			Console.WriteLine(answer);

			Console.ReadLine();
		}

		/// <summary>
		/// 約数を求めます。
		/// </summary>
		/// <param name="n">約数を求める数</param>
		/// <returns></returns>
		private static ISet<long> DivisorsFor(long n)
		{
			var divisors = new List<long>();

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

					// それぞれの約数を約数リストに追加します。
					divisors.Add(minorDivisor);
					divisors.Add(greaterDivisor);
				}

				counter++;
			}

			// 約数リストをソートして返します。
			divisors.Sort();
			return new HashSet<long>(divisors);
		}

		/// <summary>
		/// 整数のコレクションから素数を検索します。
		/// </summary>
		/// <param name="numbers"></param>
		/// <returns></returns>
		private static IEnumerable<long> FindPrimers(IEnumerable<long> numbers)
		{
			// エラトステネスの篩を利用して素数リストを取得します。
			var exproleList = new List<long>(numbers.Where(i => i != 1));
			var primers = new List<long>();

			long firstNumber = 0;
			long primersMax = 0;

			do
			{
				// 探索リストの先頭を素数リストに移動します。
				firstNumber = exproleList.First();
				primers.Add(firstNumber);

				// 探索リストから移動した素数リストの倍数を削除します。
				exproleList.RemoveAll(i => i % firstNumber == 0);

				// 探索リストが存在しなくなるか、
				// [探索リストの最大数] より [素数リストの最大数の平方] の方が大きくなるまで続けます。
				primersMax = primers.Max();
			} while (exproleList.Any() && exproleList.Max() >= primersMax * primersMax);

			// 素数リストと、探索リストに残った数が素数となります。
			return primers
				.Concat(exproleList)
				.OrderBy(i => i);
		}
	}
}
