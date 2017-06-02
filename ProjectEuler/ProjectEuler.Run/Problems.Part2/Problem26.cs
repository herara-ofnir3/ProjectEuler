using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Run.Problems.Part2
{
	/// <summary>
	/// 「逆数の循環節 その1」 
	/// 
	/// 単位分数とは分子が1の分数である. 分母が2から10の単位分数を10進数で表記すると次のようになる.
	/// 
	/// 1/2 = 0.5
	/// 1/3 = 0.(3)
	/// 1/4 = 0.25
	/// 1/5 = 0.2
	/// 1/6 = 0.1(6)
	/// 1/7 = 0.(142857)
	/// 1/8 = 0.125
	/// 1/9 = 0.(1)
	/// 1/10 = 0.1
	/// 
	/// 0.1(6)は 0.166666... という数字であり, 1桁の循環節を持つ. 1/7 の循環節は6桁ある.
	/// 
	/// d &lt; 1000 なる 1/d の中で小数部の循環節が最も長くなるような d を求めよ.	
	/// </summary>
	public class Problem26 : Problem
	{
		public override string Run()
		{
			var dmax = 1000;

			var answer = Enumerable.Range(1, dmax - 1)
				.Select(x => new { D = x, Rp = RepeatPart(F(x)) })
				.Where(x => x.Rp.Any())
				.OrderByDescending(x => x.Rp.Count())
				.First()
				.D;

			return answer.ToString();
		}

		// ReciprocalFractionalPart
		static IEnumerable<FpDigit> F(int n)
		{
			var b = 10;
			var r = 1 * b;
			
			while (r != 0)
			{
				var d = new FpDigit(
					r / n,
					r % n
					);

				yield return d;
				r = d.Rem * 10;
			}
		}

		static IEnumerable<FpDigit> RepeatPart(IEnumerable<FpDigit> fp)
		{
			var rec = new List<FpDigit>();

			foreach (var f in fp)
			{
				if (rec.Any(x => x.Rem == f.Rem))
				{
					return fp
						.Skip(rec.Count)
						.TakeWhile((x, i) => i == 0 || x.Rem != f.Rem);
				}

				rec.Add(f);
			}

			return Enumerable.Empty<FpDigit>();
		}

		struct FpDigit
		{
			public FpDigit(int q, int r)
			{
				Q = q; Rem = r;
			}

			public int Number => Q;
			public int Q { get; }
			public int Rem { get; }
		}
	}
}
