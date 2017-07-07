using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Run.Problems.Part3
{
	/// <summary>
	/// Problem 75 「1通りの整数直角三角形」
	/// ある長さの鉄線を折り曲げて3辺の長さが整数の直角三角形を作るとき, その方法が1通りしかないような最短の鉄線の長さは12cmである. 他にも沢山の例が挙げられる.
	/// 
	/// 12 cm: (3,4,5)
	/// 24 cm: (6,8,10)
	/// 30 cm: (5,12,13)
	/// 36 cm: (9,12,15)
	/// 40 cm: (8,15,17)
	/// 48 cm: (12,16,20)
	/// 
	/// それとは対照的に, ある長さの鉄線 (例えば20cm) は3辺の長さが整数の直角三角形に折り曲げることができない. また2つ以上の折り曲げ方があるものもある. 2つ以上ある例としては, 120cmの長さの鉄線を用いた場合で, 3通りの折り曲げ方がある.
	/// 
	/// 120 cm: (30,40,50), (20,48,52), (24,45,51)
	/// 
	/// Lを鉄線の長さとする. 直角三角形を作るときに1通りの折り曲げ方しか存在しないような L ≤ 1,500,000 の総数を答えよ.
	/// </summary>
	public class Problem75 : Problem
	{
		public override string Run()
		{
			var lmax = 1500000;
			var primitives = Numbers().Skip(1)
				.SelectMany(m => Enumerable.Range(1, m - 1)
					.Where(n => Satisfy(m, n))
					.Select(n => (m: m, n: n)))
				.Select(x => new { mn = x, P = Gen(x.m, x.n) })
				.Select(x => new { x.mn, x.P, L = x.P.a + x.P.b + x.P.c })
				.TakeWhile(x => x.L <= lmax * 2) // ここが雑
				.Select(x => x.L)
				.Where(l => l <= lmax)
				.ToArray();

			var triangles = new Dictionary<int, bool>(); 

			foreach (var p in primitives)
			{
				var lms = Numbers()
					.Select(n => p * n)
					.TakeWhile(lm => lm <= lmax);

				foreach (var lm in lms)
				{
					if (triangles.ContainsKey(lm))
						triangles[lm] = false;
					else
						triangles[lm] = true;
				}
			}

			var answer = triangles.Count(t => t.Value);
			return answer.ToString();
		}

		static IEnumerable<int> Numbers()
		{
			for (var n = 1; ; n++) yield return n;
		}

		static bool Satisfy(int m, int n)
		{
			return
				(n < m) &&
				((m - n) % 2 != 0) &&
				(Gcd(m, n) == 1);
		}

		static (int a, int b, int c) Gen(int m, int n)
		{
			if (!Satisfy(m, n)) throw new ArgumentException();
			var a = m * m - n * n;
			var b = 2 * m * n;
			var c = m * m + n * n;
			return (a, b, c);
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

		static int Lcm(int a, int b)
		{
			return a * b / Gcd(a, b);
		}
	}
}
