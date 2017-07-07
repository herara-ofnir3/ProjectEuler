using ProjectEuler.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Run.Problems.Part3
{
	/// <summary>
	/// Problem 58 「螺旋素数」
	/// 1から始めて, 以下のように反時計回りに数字を並べていくと, 辺の長さが7の渦巻きが形成される.
	/// 
	/// 37	36	35	34	33	32	31
	/// 38	17	16	15	14	13	30
	/// 39	18	5	4	3	12	29
	/// 40	19	6	1	2	11	28
	/// 41	20	7	8	9	10	27
	/// 42	21	22	23	24	25	26
	/// 43	44	45	46	47	48	49
	/// 面白いことに, 奇平方数が右下の対角線上に出現する. もっと面白いことには, 対角線上の13個の数字のうち, 8個が素数である. ここで割合は8/13 ≈ 62%である.
	/// 
	/// 渦巻きに新しい層を付け加えよう. すると辺の長さが9の渦巻きが出来る. 以下, この操作を繰り返していく. 対角線上の素数の割合が10%未満に落ちる最初の辺の長さを求めよ.
	/// </summary>
	public class Problem58 : Problem
	{
		public override string Run()
		{
			var primes = 0;
			var rate = 1d;

			var len = 3;
			for (len = 3; 0.1 <= rate ; len += 2)
			{
				primes += Vertices(len).Count(n => n.IsPrime());
				rate = primes / (double)(len * 2 - 1);
			}

			var answer = len - 2;
			return answer.ToString();
		}

		static IEnumerable<int> Vertices(int len)
		{
			var first = (len - 2) * (len - 2) + 1;
			yield return first + (len - 2);
			yield return first + (len - 2) + (len - 1);
			yield return first + (len - 2) + (len - 1) * 2;
			yield return first + (len - 2) + (len - 1) * 3;
		}
	}
}
