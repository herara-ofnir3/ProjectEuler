using ProjectEuler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace ProjectEuler.Run.Problems
{
	/// <summary>
	/// 「1000桁のフィボナッチ数」
	/// 
	/// フィボナッチ数列は以下の漸化式で定義される:
	/// 
	/// Fn = Fn-1 + Fn-2, ただし F1 = 1, F2 = 1.
	/// 最初の12項は以下である.
	/// 
	/// F1 = 1
	/// F2 = 1
	/// F3 = 2
	/// F4 = 3
	/// F5 = 5
	/// F6 = 8
	/// F7 = 13
	/// F8 = 21
	/// F9 = 34
	/// F10 = 55
	/// F11 = 89
	/// F12 = 144
	/// 12番目の項, F12が3桁になる最初の項である.
	/// 
	/// 1000桁になる最初の項の番号を答えよ.	
	/// </summary>
	public class Problem25 : Problem
	{
		public override string Run()
		{
			var digits = 1000;

			var fibonacci = Fibonacci().Select((x, i) => new { Number = x, Index = i });
			foreach (var f in fibonacci)
			{
				if (f.Number.Digits().Count() == digits)
				{
					return (f.Index + 1).ToString();
				}
			}

			throw new InvalidOperationException();
		}

		static IEnumerable<BigInteger> Fibonacci()
		{
			BigInteger previous = 1;
			BigInteger current = 0;
			BigInteger next = 0;

			while (true)
			{
				next = previous + current;
				yield return next;

				previous = current;
				current = next;
			}
		}

		/*
		static int Fibonacci(int n)
		{
			if (n <= 0) throw new ArgumentOutOfRangeException("n");
			if (n == 1) return 1;
			if (n == 2) return 1;
			return Fibonacci(n - 1) + Fibonacci(n - 2);
		}*/
	}
}
