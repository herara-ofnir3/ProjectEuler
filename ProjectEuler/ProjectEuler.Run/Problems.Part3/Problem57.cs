using ProjectEuler.Core;
using System.Linq;
using System.Numerics;

namespace ProjectEuler.Run.Problems.Part3
{
	/// <summary>
	/// Problem 57 「平方根の近似分数」
	/// 2の平方根は無限に続く連分数で表すことができる.
	/// 
	/// √ 2 = 1 + 1/(2 + 1/(2 + 1/(2 + ... ))) = 1.414213...
	/// 最初の4回の繰り返しを展開すると以下が得られる.
	/// 
	/// 1 + 1/2 = 3/2 = 1.5
	/// 1 + 1/(2 + 1/2) = 7/5 = 1.4
	/// 1 + 1/(2 + 1/(2 + 1/2)) = 17/12 = 1.41666...
	/// 1 + 1/(2 + 1/(2 + 1/(2 + 1/2))) = 41/29 = 1.41379...
	/// 
	/// 次の3つの項は99/70, 239/169, 577/408である. 第8項は1393/985である. これは分子の桁数が分母の桁数を超える最初の例である.
	/// 
	/// 最初の1000項を考えたとき, 分子の桁数が分母の桁数を超える項はいくつあるか?
	/// </summary>
	public class Problem57 : Problem
	{
		public override string Run()
		{
			// BigIntegerの力でごり押し。
			var answer = Enumerable.Range(1, 1000)
				.Select(n => F(n, (1, 2)))
				.Count(f => f.denom.Digits().Count() < f.num.Digits().Count());

			return answer.ToString();
		}

		static (BigInteger num, BigInteger denom) F(BigInteger n, (BigInteger num, BigInteger denom) f)
		{
			if (n == 1)
				return Add(1, f);

			return F(n - 1, Div(1, Add(2, f)));
		}

		static (BigInteger num, BigInteger denom) Add(BigInteger n, (BigInteger num, BigInteger denom) f)
		{
			return (n * f.denom + f.num, f.denom);
		}

		static (BigInteger num, BigInteger denom) Div(BigInteger l, (BigInteger num, BigInteger denom) r)
		{
			return (l * r.denom, r.num);
		}
	}
}
