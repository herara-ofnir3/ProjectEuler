using ProjectEuler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler.Run.Problems.Part2
{
	/// <summary>
	/// Problem 38 「パンデジタル倍数」
	/// 192 に 1, 2, 3 を掛けてみよう.
	/// 
	/// 192 × 1 = 192
	/// 192 × 2 = 384
	/// 192 × 3 = 576
	/// 
	/// 積を連結することで1から9の パンデジタル数 192384576 が得られる. 192384576 を 192 と (1,2,3) の連結積と呼ぶ.
	/// 
	/// 同じようにして, 9 を 1,2,3,4,5 と掛け連結することでパンデジタル数 918273645 が得られる. これは 9 と (1,2,3,4,5) との連結積である.
	/// 
	/// 整数と (1,2,...,n) (n > 1) との連結積として得られる9桁のパンデジタル数の中で最大のものはいくつか?
	/// </summary>
	public class Problem38 : Problem
	{
		public override string Run()
		{
			var sample = Enumerable.Range(1, 9).ToArray();
			var found = new List<int>();
			foreach (var n in Enumerable.Range(1, 9876).Reverse())
			{
				var d = n.Digits().ToList();

				foreach (var m in Enumerable.Range(2, 8))
				{
					if (9 <= d.Count())
						break;

					var multi = n * m;
					var md = multi.Digits();

					if (d.Intersect(md).Any())
						break;

					d.InsertRange(0, md);
				}

				if (d.OrderBy(x => x).SequenceEqual(sample))
				{
					found = d;
					break;
				}
			}

			var answer = DigitsToInt(found);
			return answer.ToString();
		}

		static int DigitsToInt(IEnumerable<int> digits)
		{
			return digits.Select((x, i) => x * (int)Math.Pow(10, i)).Sum();
		}
	}
}
