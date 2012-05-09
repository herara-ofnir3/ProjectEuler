using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler.Problem10
{
	class Program
	{
		/// <summary>
		/// 200万以下の全ての素数の和を計算する。
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			int n = 2000000;

			// エラトステネスの篩を利用して素数リストを取得します。
			var numbers = new List<int>(Enumerable.Range(2, n - 2));
			var primers = new List<int>();

			int firstNumber = 0;
			int primersMax = 0;

			do
			{
				// 探索リストの先頭を素数リストに移動します。
				firstNumber = numbers.First();
				primers.Add(firstNumber);

				// 探索リストから移動した素数リストの倍数を削除します。
				numbers.RemoveAll(i => i % firstNumber == 0);

				// 探索リストが存在しなくなるか、
				// [探索リストの最大数] より [素数リストの最大数の平方] の方が大きくなるまで続けます。
				primersMax = primers.Max();
			} while (numbers.Any() && numbers.Max() >= primersMax * primersMax);

			// 素数リストと探索リストに残った数が素数となります。
			// 素数リストと探索リストに残った数を連結し、合計を求めます。
			long answer = primers
				.Concat(numbers)
				.Select(i => (long)i)
				.Sum();

			Console.WriteLine(answer);
			Console.ReadLine();
		}
	}
}
