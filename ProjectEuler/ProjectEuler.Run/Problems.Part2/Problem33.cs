using ProjectEuler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler.Run.Problems.Part2
{
	/// <summary>
	/// Problem 33 「桁消去分数」 †
	/// 49/98は面白い分数である.「分子と分母からそれぞれ9を取り除くと, 49/98 = 4/8 となり, 簡単な形にすることができる」と経験の浅い数学者が誤って思い込んでしまうかもしれないからである. (方法は正しくないが，49/98の場合にはたまたま正しい約分になってしまう．)
	/// 
	/// 我々は 30/50 = 3/5 のようなタイプは自明な例だとする.
	/// 
	/// このような分数のうち, 1より小さく分子・分母がともに2桁の数になるような自明でないものは, 4個ある.
	/// 
	/// その4個の分数の積が約分された形で与えられたとき, 分母の値を答えよ.
	/// </summary>
	public class Problem33 : Problem
	{
		public override string Run()
		{
			var d = DivPatterns()
				.Where(x => F(x.l, x.r))
				.Aggregate((a, b) => (l: a.l * b.l, r: a.r * b.r));

			var gcd = Gcd(d.l, d.r);
			var answer = d.r / gcd;
			return answer.ToString();
		}

		static IEnumerable<(int l, int r)> DivPatterns()
		{
			for (var l = 10; l <= 99; l++)
			{
				for (var r = l + 1; r <= 99; r++)
				{
					yield return (l, r);
				}
			}
		}

		static bool F(int l, int r)
		{
			if (Gcd(l, r) == 1)
				return false;

			var ld = l.Digits().ToArray();
			var rd = r.Digits().ToArray();

			if (!(ld[0] == rd[1] || ld[1] == rd[0]))
				return false;

			var c = ld.Intersect(rd).First();

			var exL = ld.FirstOrDefault(x => x != c);
			var exR = rd.FirstOrDefault(x => x != c);

			return exR != 0 && l / (decimal)r == exL / (decimal)exR;
		}

		static int Gcd(int a, int b)
		{
			if (a == 0 || b == 0)
				return 0;

			var h = a;
			var l = b;

			if (h < l)
			{
				h = b;
				l = a; 
			}

			var r = h % l;
			while (r != 0)
			{
				h = l;
				l = r;
				r = h % l;
			}

			return l;
		}
	}
}
