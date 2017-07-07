using ProjectEuler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler.Run.Problems.Part3
{
	/// <summary>
	/// Problem 65 「e の近似分数」
	/// 2の平方根は無限連分数として書くことができる.
	/// 
	/// 無限連分数である √2 = [1;(2)] と書くことができるが, (2) は2が無限に繰り返されることを示す. 同様に, √23 = [4;(1,3,1,8)].
	/// 
	/// 平方根の部分的な連分数の数列から良い有理近似が得られることが分かる.√2の近似分数について考えよう.
	/// 
	/// 従って, √2の近似分数からなる数列の最初の10項は：
	/// 
	/// 1, 3/2, 7/5, 17/12, 41/29, 99/70, 239/169, 577/408, 1393/985, 3363/2378, ...
	/// 
	/// もっとも驚くべきことに, 数学的に重要な定数,
	/// e = [2; 1,2,1, 1,4,1, 1,6,1 , ... , 1,2k,1, ...].
	/// 
	/// e の近似分数からなる数列の最初の10項は：
	/// 
	/// 2, 3, 8/3, 11/4, 19/7, 87/32, 106/39, 193/71, 1264/465, 1457/536, ...
	/// 
	/// 10項目の近似分数の分子の桁を合計すると1+4+5+7=17である.
	/// 
	/// e についての連分数である近似分数の100項目の分子の桁の合計を求めよ.
	/// </summary>
	public class Problem65 : Problem
	{
		public override string Run()
		{
			var answer = E(100).num.Digits().Sum();
			return answer.ToString();
		}

		static int A(int n)
		{
			if (n == 1)
				return 0;

			var r = n % 3;
			if (r != 0)
				return 1;

			var q = n / 3;
			return q * 2;
		}

		static (BigInteger num, BigInteger denom) E(int n)
		{
			if (n == 1)
				return (2, 1);

			return E(n - 1, (1, A(n)));
		}

		static (BigInteger BigInteger, BigInteger denom) E(int n, (BigInteger num, BigInteger denom) f)
		{
			if (n == 1)
				return Add(2, f);

			return E(n - 1, Div(1, Add(A(n), f)));
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
