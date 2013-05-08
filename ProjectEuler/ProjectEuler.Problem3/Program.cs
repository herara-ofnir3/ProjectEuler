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
			var n = 600851475143;

			// まず約数を求め、その中から素数を見つけることで素因数を獲得します。
			var answer = n.Divisors().Primers().Max();
			Console.WriteLine(answer);

			Console.ReadLine();
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

		public static IEnumerable<long> Primers(this IEnumerable<long> numbers)
		{
			// エラトステネスの篩を利用して素数リストを取得します。
			var exproleList = new List<long>(numbers.Where(i => i != 1).OrderBy(i => i));
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
